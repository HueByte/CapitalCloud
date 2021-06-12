using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using API.Authentication;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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

    public class ChatEvents
    {
        public const string OnReceiveMessage = "OnReceiveMessage";
        public const string OnUserConnected = "OnUserConnected";
        public const string OnJoinSession = "OnJoinSession";
        public const string OnUserDisconnected = "OnUserDisconnected";
    }

    public class ChatHub : Hub
    {
        public static List<ChatUser> chatUsers = new List<ChatUser>(); // Consider adding List<string> connectionIds instead of new instance for each connection of the same user
        public static List<string> anonConnections = new List<string>();
        public static List<Message> messages = new List<Message>();
        private readonly ILogger<ChatHub> _logger;
        private readonly IJwtAuthentication _jwtAuth;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatHub(ILogger<ChatHub> logger, IJwtAuthentication jwtAuth, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _jwtAuth = jwtAuth;
            _userManager = userManager;

            //first message 
            messages.Add(new Message() { User = new ChatUser("admin", "https://upload.wikimedia.org/wikipedia/commons/7/7c/Cima_da_Conegliano%2C_God_the_Father.jpg", "0", 999), Content = "Hello World" });
        }

        [Authorize]
        public async Task SendMessage(string content)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                try
                {
                    // limit of saved messages
                    if (messages.Count >= 1000) messages.Remove(messages.First());
                    // get user from connections
                    var user = chatUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

                    var message = new Message() { User = user, Content = content };
                    messages.Add(message);

                    await Clients.All.SendAsync(ChatEvents.OnReceiveMessage, message);
                }
                catch (Exception e) { _logger.Log(LogLevel.Error, e.Message); }
            }
        }

        [Authorize]
        public async Task OnConnected(string token, string connectionId)
        {
            //new 
            if (!string.IsNullOrWhiteSpace(token))
            {
                var email = _jwtAuth.GetEmailFromToken(token);
                var user = await _userManager.FindByEmailAsync(email);
                chatUsers.Add(new ChatUser(user.UserName, connectionId, user.Avatar_Url, Convert.ToUInt32(user.exp)));
            }

            await Clients.All.SendAsync(ChatEvents.OnUserConnected, (chatUsers.GroupBy(u => u.Username).Select(e => e.First()).Count() + anonConnections.Count()));
            await Clients.Caller.SendAsync(ChatEvents.OnJoinSession, messages);
        }

        public async Task OnConnectedAnon(string connectionId)
        {
            // TODO - Maybe add public IP checker to avoid fake user count 
            try
            {
                anonConnections.Add(Context.ConnectionId);
                // TODO - do something with this, user count isn't unique 
                await Clients.All.SendAsync(ChatEvents.OnUserConnected, (chatUsers.GroupBy(u => u.Username).Select(e => e.First()).Count() + anonConnections.Count));
                await Clients.Caller.SendAsync(ChatEvents.OnJoinSession, messages);

                _logger.Log(LogLevel.Information, "Anon connected");
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

            Clients.All.SendAsync(ChatEvents.OnUserDisconnected).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(e);
        }
    }
}