using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace API.Hubs
{
    public class Message
    {
        public string User { get; set; }
        public string Content { get; set; }
    }

    public class ChatHub : Hub
    {
        public static readonly Dictionary<string, string> users = new Dictionary<string, string>();
        public static List<Message> messages = new List<Message>();
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }
        public Task SendMessage(string user, string content)
        {
            if(messages.Count >= 1000) messages.Remove(messages.First());
            messages.Add(new Message() { User = user, Content = content });
            return Clients.All.SendAsync("OnReceiveMessage", user, content);
        }

        public async Task OnConnected(string userName, string id)
        {
            if(!users.Values.Any(entry => entry == userName))
                users.Add(id, userName);
            
            _logger.Log(LogLevel.Information, $"{userName} connected to the chat");
            await Clients.All.SendAsync("OnUserConnected");
            await Clients.Caller.SendAsync("OnJoinSession", messages);
        }

        public override Task OnConnectedAsync()
        {
            if(users.TryGetValue(Context.ConnectionId, out string userName)) 
                _logger.Log(LogLevel.Information, $"Client disconnected {userName}");
            else
                _logger.Log(LogLevel.Error, $"Client disconnected and couldn't get his username");

            return base.OnConnectedAsync();
        }
    }
}