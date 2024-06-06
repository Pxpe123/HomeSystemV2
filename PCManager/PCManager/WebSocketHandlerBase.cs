using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

public abstract class WebSocketHandlerBase
{
    protected WebSocketConnectionManager WebSocketConnectionManager { get; }

    public WebSocketHandlerBase(WebSocketConnectionManager webSocketConnectionManager)
    {
        WebSocketConnectionManager = webSocketConnectionManager;
    }

    public virtual async Task OnConnected(WebSocket socket)
    {
        await WebSocketConnectionManager.AddSocketAsync(socket);
    }

    public virtual async Task OnDisconnected(WebSocket socket)
    {
        await WebSocketConnectionManager.RemoveSocketAsync(socket);
    }

    public abstract Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);

    protected async Task SendAsync(WebSocket socket, string message)
    {
        if (socket.State != WebSocketState.Open)
            return;

        await socket.SendAsync(Encoding.UTF8.GetBytes(message), WebSocketMessageType.Text, true, CancellationToken.None);
    }
}