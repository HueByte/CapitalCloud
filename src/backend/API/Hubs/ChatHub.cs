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
        public static List<ChatUser> chatUsers = new List<ChatUser>(); // Consider adding List<string> connectionIds instead of new instance for each connection of the same user
        public static List<string> anonConnections = new List<string>();
        public static List<Message> messages = new List<Message>();
        private readonly ILogger<ChatHub> _logger;

        public ChatHub(ILogger<ChatHub> logger)
        {
            _logger = logger;
        }

        [Authorize]
        public async Task SendMessage(string content)
        {
            if (!string.IsNullOrEmpty(content))
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
        }

        // TODO - Consider making HTTP request every 10seconds instead of websocket
        // that would improve effectiveness and resource performance 
        public async Task OnConnected(string username, string id, uint xp = 0, string avatar_url = "")
        {
            lock (chatUsers)
            {
                // if (!chatUsers.Any(entry => entry.Username == username))
                chatUsers.Add(new ChatUser(username, id, avatar_url, xp));
            }

            _logger.Log(LogLevel.Information, $"{username} connected to the chat");
            await Clients.All.SendAsync("OnUserConnected", (chatUsers.GroupBy(u => u.Username).Select(e => e.First()).Count() + anonConnections.Count));
            await Clients.Caller.SendAsync("OnJoinSession", messages);
        }

        public async Task OnConnectedAnon(string connectionId)
        {
            // TODO - Maybe add public IP checker to avoid fake user count 
            try
            {
                anonConnections.Add(Context.ConnectionId);
                _logger.Log(LogLevel.Information, "Anon connected");
                // TODO - do something with this, user count isn't unique 
                await Clients.All.SendAsync("OnUserConnected", (chatUsers.GroupBy(u => u.Username).Select(e => e.First()).Count() + anonConnections.Count));
                await Clients.Caller.SendAsync("OnJoinSession", messages);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
            }
        }

        public override Task OnDisconnectedAsync(Exception e)
        {
            // TODO - uhh think about anon disconnecting 
            ChatUser user = chatUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            if (user != null)
            {
                _logger.Log(LogLevel.Information, $"Client disconnected {user.Username}");
                chatUsers.Remove(user);
            }
            else
            {
                try
                {
                    anonConnections.Remove(Context.ConnectionId);
                    _logger.Log(LogLevel.Information, "Disconnected anon user");
                }
                catch (Exception err)
                {
                    _logger.Log(LogLevel.Error, $"Disconnected client which wasn't anon or user {err.Message}");
                }
            }
            Clients.All.SendAsync("OnUserDisconnected").GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(e);
        }
    }
}