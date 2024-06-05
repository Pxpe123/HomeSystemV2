using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AssaultCubeExternal;

namespace PCManager
{
    [ApiController]
    [Route("[controller]")]
    public class LoggingController : ControllerBase
    {
        private readonly AssaultCubeMain _assaultCubeExternal;
        private readonly HttpClient _httpClient;

        public LoggingController(AssaultCubeMain assaultCubeExternal)
        {
            _assaultCubeExternal = assaultCubeExternal;
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public IActionResult Get([FromBody] JsonElement requestData)
        {
            return LogRequest("GET", requestData);
        }

        [HttpPut]
        public IActionResult Put([FromBody] JsonElement requestData)
        {
            return LogRequest("PUT", requestData);
        }

        private IActionResult LogRequest(string method, JsonElement requestData)
        {
            string logMessage = $"{DateTime.Now}: Received {method} request from {Request.HttpContext.Connection.RemoteIpAddress}";

            if (requestData.ValueKind == JsonValueKind.Object)
            {
                Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

                foreach (var item in requestData.EnumerateObject())
                {
                    keyValuePairs.Add(item.Name, item.Value.ToString());
                }

                foreach (var kvp in keyValuePairs)
                {
                    logMessage += $", {kvp.Key}: {kvp.Value}";
                }
            }

            Console.WriteLine(logMessage);

            if (requestData.TryGetProperty("Type", out JsonElement type))
            {
                if (type.GetString() == "Game")
                {
                    return HandleGamePut(requestData);
                }
            }

            if (requestData.TryGetProperty("GetData", out JsonElement game))
            {
                if (game.GetString() == "DerailValley")
                {
                    return GetDerailValleyDataAsync().Result; // Wait synchronously for the task to complete
                }
            }

            return Ok(logMessage);
        }

        private IActionResult HandleGamePut(JsonElement jsonData)
        {
            JObject jObject = JObject.Parse(jsonData.GetRawText());

            JToken gameToken = jObject["Game"];
            if (gameToken != null)
            {
                string gameName = gameToken.ToString();
                Console.WriteLine($"Game property value: {gameName}");
                if (gameName == "AssaultCube")
                {
                    return AssaultCubeHandler(jObject);
                }
            }
            else
            {
                Console.WriteLine("Game property not found in the JSON data.");
            }

            return BadRequest("Invalid Game Type");
        }

        private IActionResult AssaultCubeHandler(JObject jObject)
        {
            JToken valueToken = jObject["Value"];
            string value = valueToken.ToString();

            switch (value)
            {
                case "NoRecoil":
                    _assaultCubeExternal.Vars.ToggleNoRecoil();
                    return Ok("No Recoil toggled");
                case "InfAmmo":
                    _assaultCubeExternal.Vars.ToggleInfAmmo();
                    return Ok("Infinite Ammo toggled");
                case "GodMode":
                    _assaultCubeExternal.Vars.ToggleGodMode();
                    Console.WriteLine(_assaultCubeExternal.Vars.bGodMode);
                    return Ok("God Mode toggled");
                case "RefillHealth":
                    _assaultCubeExternal.PlayerFunctions.FillArmourHealth();
                    return Ok("Health and Armour refilled");
                case "RefillAmmo":
                    _assaultCubeExternal.PlayerFunctions.ResetAmmo();
                    return Ok("Ammo Reset");
                default:
                    return BadRequest("Invalid Value");
            }
        }

        private async Task<IActionResult> GetDerailValleyDataAsync()
        {
            string url = "http://localhost:30152/";
            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                return Ok(responseData);
            }
            else
            {
                return StatusCode((int)response.StatusCode, $"Failed to fetch data. Status code: {response.StatusCode}");
            }
        }


    }
}
