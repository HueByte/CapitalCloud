using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace API.Hubs
{
    public class MessageTest
    {
        public string User { get; set; }
        public string Content { get; set; }
    }

    public class TestHub : Hub
    {
        public static readonly Dictionary<string, string> users = new Dictionary<string, string>();
        public static List<MessageTest> sessionMessages = new List<MessageTest>();

        [Authorize]
        public Task SendMessage(string user, string content)
        {
            sessionMessages.Add(new MessageTest() { User = user, Content = content });
            return Clients.All.SendAsync("OnReceiveMessage", user, content); // event
        }

        public async Task OnConnected(string userName, string id)
        {
            //Add user
            if(!users.Values.Any(entry => entry == userName))
                users.Add(id, userName);

            await Clients.All.SendAsync("OnUserConnected", users.Values.ToArray()); // event
            await Clients.Caller.SendAsync("OnJoinSession", sessionMessages); // event

            Console.WriteLine($"Client connected {userName} {id}");
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine($"Client disconnected {users[Context.ConnectionId]}");
            users.Remove(Context.ConnectionId);

            Clients.All.SendAsync("OnUserDisconnected", users.Values.ToArray()).GetAwaiter().GetResult(); // event

            if(users.Count == 0)
                sessionMessages.Clear();

            return base.OnDisconnectedAsync(exception);
        }
    }
}