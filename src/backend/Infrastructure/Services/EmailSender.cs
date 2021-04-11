using System;
using System.IO;
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
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SmtpClient _smtp;
        public EmailSender(IConfiguration config, UserManager<ApplicationUser> userManager, SmtpClient smtp)
        {
            _smtp = smtp;
            _userManager = userManager;
            _config = config;
        }
        public async Task SendActivationEmail(string email, string url)
        {
            // TODO - Do proper message
            string messageBody = File.ReadAllText("HTMLFile/ActivationEmail.html");
            messageBody = messageBody.Replace("%ActivationLink%", url);
            MailMessage message = new MailMessage()
            {
                From = new MailAddress("cloudbytesdonotreply@gmail.com", "CloudBytes - Do not Reply"),
                Subject = "Awful Mail Activation",
                Body = messageBody,
                IsBodyHtml = true
            };
            message.To.Add(new MailAddress(email, "New Account!"));
            try
            {
                await _smtp.SendMailAsync(message);
                Log.Information("Sended activation mail for: " + email);
            }
            catch (Exception ex)
            {
                Log.Error("Error occurs during sending mail: " + ex.Message);
            }
        }
    }
}