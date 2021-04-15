using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class TestHub : Hub
    {
        public Task SendMessage(string user, string message) 
        {
            return Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}