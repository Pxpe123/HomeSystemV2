using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;

public class WebSocketConnectionManager
{
    private readonly ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

    public void AddSocket(string id, WebSocket socket)
    {
        _sockets.TryAdd(id, socket);
    }

    public async Task AddSocketAsync(WebSocket socket)
    {
        AddSocket(Guid.NewGuid().ToString(), socket);
    }

    public WebSocket GetSocketById(string id)
    {
        _sockets.TryGetValue(id, out WebSocket socket);
        return socket;
    }

    public ConcurrentDictionary<string, WebSocket> GetAllSockets()
    {
        return _sockets;
    }

    public string GetId(WebSocket socket)
    {
        return _sockets.FirstOrDefault(p => p.Value == socket).Key;
    }

    public async Task RemoveSocketAsync(WebSocket socket)
    {
        _sockets.TryRemove(GetId(socket), out WebSocket removedSocket);
        await removedSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Socket connection closed", CancellationToken.None);
    }
}