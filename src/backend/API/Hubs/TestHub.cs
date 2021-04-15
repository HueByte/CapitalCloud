using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class TestHub : Hub
    {
        public Task SendMessage(string user, string message)
        {
            Console.WriteLine(message);
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendID(string id)
        {
            await Clients.All.SendAsync("setClientMessage", $"A new connection {id}");
            Console.WriteLine("Client connected " + id);
        }
    }
}