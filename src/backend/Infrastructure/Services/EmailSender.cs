using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Core.Entities;
using Core.ServiceInterfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Infrastructure.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfirmationTokenRepository _emailrepo;
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        public EmailSender(IConfiguration config, EmailConfirmationTokenRepository emailrepo, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _config = config;
            _emailrepo = emailrepo;

        }
        public async Task SendActivationEmail(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            SmtpClient client = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = _config.GetValue<string>("Passwords:gmail-client-username"),
                    Password = _config.GetValue<string>("Passwords:gmail-client-password")
                }

            };
            MailAddress basic = new MailAddress("cloudbytesdonotreply@gmail.com", "Do Not Reply");
            MailAddress reciver = new MailAddress(user.Email, "New Account!");
            MailMessage message = new MailMessage()
            {
                From = basic,
                Subject = "Awful Mail Activation",
                Body = $"It's just awful mail, don't care",
                IsBodyHtml=true
            };
            message.To.Add(reciver);
            try
            {
                await client.SendMailAsync(message);
                Log.Information("Sended activation mail for: " + user.Id);
                await _emailrepo.InsertOne(new EmailConfirmationToken(){userId = user.Id,token = token, expiredAt = DateTime.Now.AddHours(24)});
            }
            catch (Exception ex)
            {
                Log.Error("Error occurs during sending mail: ||" + ex.Message);

            }
        }
    }
}