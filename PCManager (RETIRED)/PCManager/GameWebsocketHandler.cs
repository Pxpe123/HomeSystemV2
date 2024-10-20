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
    private DerailValley.DerailValley _derailValley;
    private MusicHandler _musicHandler;

    public GameWebSocketHandler(WebSocketConnectionManager webSocketConnectionManager, LoggingController loggingController, AssaultCubeMain assaultCubeExternal) : base(webSocketConnectionManager)
    {
        _musicHandler = new MusicHandler();
        _loggingController = loggingController;
        _assaultCubeExternal = assaultCubeExternal; // Initialize _assaultCubeExternal
    }

public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
{
    if (_derailValley == null)
    {
        _derailValley = new DerailValley.DerailValley();
    }

    if (_musicHandler == null)
    {
        _musicHandler = new MusicHandler();
    }
    
    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

    JObject parsedMessage = JObject.Parse(message);
    if (parsedMessage != null && parsedMessage.ContainsKey("Type"))
    {
        string Type = parsedMessage["Type"].ToString();

        if (Type == "MusicData")
        {
            _musicHandler.HandleMusicData(parsedMessage);
        }
        else if (Type == "GetAudioData")
        {
            Console.WriteLine("asdasd");
            JObject audioData = _musicHandler.GetAudioData();
            string jsonData = JsonConvert.SerializeObject(audioData);
            byte[] dataBuffer = Encoding.UTF8.GetBytes(jsonData);
            await socket.SendAsync(new ArraySegment<byte>(dataBuffer, 0, dataBuffer.Length),
                WebSocketMessageType.Text, true, CancellationToken.None);
        }
        else if (Type == "PutData")
        {
            string Game = parsedMessage["Game"].ToString();

            if (Game == "DerailValley")
            {
                try
                {
                    if (_derailValley != null)
                    {
                        _derailValley.SetGameData(parsedMessage as JObject);
                    }
                    else
                    {
                        Console.WriteLine("Null");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
        else if (Type == "Game")
        {
            string Game = parsedMessage["Game"].ToString();

            if (Game == "DerailValley")
            {
                JObject gameData = _derailValley.GetGameData();
                string jsonData = JsonConvert.SerializeObject(gameData);
                byte[] dataBuffer = Encoding.UTF8.GetBytes(jsonData);
                await socket.SendAsync(new ArraySegment<byte>(dataBuffer, 0, dataBuffer.Length),
                    WebSocketMessageType.Text, true, CancellationToken.None);
            }
            
            if (Game == "AssaultCube")
            {
                string Value = parsedMessage["Value"].ToString();

                switch (Value)
                {
                    case "GetPlayerData":
                        JObject gameData = _assaultCubeExternal.PlayerFunctions.ReadGameData();
                        if (gameData != null)
                        {
                            string jsonData = JsonConvert.SerializeObject(gameData);
                            byte[] dataBuffer = Encoding.UTF8.GetBytes(jsonData);
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