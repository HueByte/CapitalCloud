using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace API.Hubs
{
    public class Message
    {
        public ChatUser User { get; set; }
        public string Content { get; set; }
    }

    public class ChatUser
    {
        public ChatUser(string username, string connectionId, string avatar_url = "", uint xp = 0)
        {
            ConnectionId = connectionId;
            Username = username;
            AvatarUrl = avatar_url;
            Level = Convert.ToUInt32(Math.Floor(Math.Floor(25 + Math.Sqrt(625 + 100 * xp)) / 50));
        }

        public string ConnectionId { get; set; }
        public string Username { get; set; }
        public string AvatarUrl { get; set; }
        public uint Level { get; set; }
    }

    public class ChatHub : Hub
    {
        public static List<ChatUser> chatUsers = new List<ChatUser>();
        public static List<Message> messages = new List<Message>();
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public async Task SendMessage(string content)
        {
            try
            {
                if (messages.Count >= 1000) messages.Remove(messages.First());
                var user = chatUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
                var message = new Message() { User = user, Content = content };
                messages.Add(message);
                await Clients.All.SendAsync("OnReceiveMessage", message);
            }
            catch (Exception e) { Console.WriteLine(e); }
        }

        // TODO - Consider making HTTP request every 10seconds instead of websocket
        // that would improve effectiveness and resource performance 
        public async Task OnConnected(string username, string id, uint xp = 0, string avatar_url = "")
        {
            lock (chatUsers)
            {
                if (!chatUsers.Any(entry => entry.Username == username))
                    chatUsers.Add(new ChatUser(username, id, avatar_url, xp));
            }

            _logger.Log(LogLevel.Information, $"{username} connected to the chat");
            await Clients.All.SendAsync("OnUserConnected", chatUsers.Count);
            await Clients.Caller.SendAsync("OnJoinSession", messages);
        }

        public async Task OnConnectedAnon(string connectionId)
        {
            // TODO - Maybe add public IP checker to avoid fake user count 
            chatUsers.Add(new ChatUser("anon", Context.ConnectionId));
            await Clients.All.SendAsync("OnUserConnected", chatUsers.Count);
        }

        public override Task OnDisconnectedAsync(Exception e)
        {
            ChatUser user = chatUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            _logger.Log(LogLevel.Information, $"Client disconnected {user.Username}");
            chatUsers.Remove(user);
            Clients.All.SendAsync("OnUserDisconnected").GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(e);
        }
    }
}