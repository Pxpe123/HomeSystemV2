using System.Net.WebSockets;
using System.Text;
using AssaultCubeExternal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PCManager;

public class GameWebSocketHandler : WebSocketHandlerBase
{
    private readonly LoggingController _loggingController;
    private readonly AssaultCubeMain _assaultCubeExternal;

    public GameWebSocketHandler(WebSocketConnectionManager webSocketConnectionManager, LoggingController loggingController, AssaultCubeMain assaultCubeExternal) : base(webSocketConnectionManager)
    {
        _loggingController = loggingController;
        _assaultCubeExternal = assaultCubeExternal; // Initialize _assaultCubeExternal
    }

public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
{
    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

    var parsedMessage = JObject.Parse(message);
    if (parsedMessage != null && parsedMessage.ContainsKey("Type") && parsedMessage.ContainsKey("Game") && parsedMessage.ContainsKey("Value"))
    {
        string Type = parsedMessage["Type"].ToString();

        if (Type == "Game")
        {
            string Game = parsedMessage["Game"].ToString();

            if (Game == "AssaultCube")
            {
                string Value = parsedMessage["Value"].ToString();

                switch (Value)
                {
                    case "GetPlayerData":
                        JObject gameData = _assaultCubeExternal.PlayerFunctions.ReadGameData();
                        if (gameData != null)
                        {
                            // Serialize gameData to JSON
                            string jsonData = JsonConvert.SerializeObject(gameData);
                            // Convert JSON string to byte array
                            byte[] dataBuffer = Encoding.UTF8.GetBytes(jsonData);
                            // Send the data back through the WebSocket connection
                            await socket.SendAsync(new ArraySegment<byte>(dataBuffer, 0, dataBuffer.Length),
                                WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                        else
                        {
                    }
                        break;
                    case "NoRecoil":
                        _assaultCubeExternal.Vars.ToggleNoRecoil();
                        break;
                    case "InfAmmo":
                        _assaultCubeExternal.Vars.ToggleInfAmmo();
                        break;
                    case "GodMode":
                        _assaultCubeExternal.Vars.ToggleGodMode();
                        break;
                    case "RefillHealth":
                        _assaultCubeExternal.PlayerFunctions.FillArmourHealth();
                        break;
                    case "RefillAmmo":
                        _assaultCubeExternal.PlayerFunctions.ResetAmmo();
                        break;
                    default:
                        break;
                }
            }
        }
        else
        {
            Console.WriteLine("Data not Given");
        }
    }
}
}