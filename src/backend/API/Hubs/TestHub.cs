using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class TestHub : Hub
    {
        public static readonly Dictionary<string, string> users = new Dictionary<string, string>();

        public Task SendMessage(string user, string message)
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message); // event
        }

        public async Task OnConnected(string userName, string id)
        {
            //Add user
            users.Add(id, userName);

            await Clients.All.SendAsync("UserConnected", users.Values.ToArray()); // event

            Console.WriteLine($"Client connected {userName} {id}");
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"Client disconnected {users[Context.ConnectionId]}");
            users.Remove(Context.ConnectionId);

            Clients.All.SendAsync("UserDisconnected", users.Values.ToArray()).GetAwaiter().GetResult(); // event

            return base.OnDisconnectedAsync(exception);
        }
    }
}