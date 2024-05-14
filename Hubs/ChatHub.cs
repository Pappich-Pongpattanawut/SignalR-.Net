using FormulaOne.ChatService.Models;
using Microsoft.AspNetCore.SignalR;
using FormulaOne.ChatService.DataService;

namespace FormulaOne.ChatService.Hubs;

public class ChatHub : Hub
{
    private readonly SharedDb _sheared;

    public ChatHub(SharedDb shared) => _sheared = shared;

    public async Task JoinSpecificChatRoom(UserConnection connection)
    {
        //add user connection in room chat
        await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);
        //add connection in class SharedDb
        _sheared.Connections[Context.ConnectionId] = connection; 

        await Clients.Group(connection.ChatRoom)
            .SendAsync("JoinSpecificChatRoom", "admin", $"{connection.Username} has joined {connection.ChatRoom}");
    }

    public async Task SendMessage(string msg)
    {
        //check user chat room and send
        if(_sheared.Connections.TryGetValue(Context.ConnectionId, out UserConnection _connection)) 
        {   
            await Clients.Group(_connection.ChatRoom).SendAsync("ReceiveSpecificMessage", _connection.Username, msg); 
        }
    }
}


