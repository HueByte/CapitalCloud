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

        private readonly SmtpClient _smtp;
        public EmailSender(SmtpClient smtp)
        {
            _smtp = smtp;
        }
        public async Task<bool> SendEmailAsync(MailMessage message, MailAddress address)
        {
            message.From = new MailAddress("cloudbytesdonotreply@gmail.com", "CloudBytes - Do not Reply");
            message.To.Add(address);
            try
            {
                await _smtp.SendMailAsync(message);
                Log.Information("Sended mail for: " + address.Address);
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("Error occurs during sending mail: " + ex.Message);
                return false;
            }
        }
    }
}