using System.Net.Mail;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.ServiceInterfaces
{
    public interface IEmailSender
    {
        Task<bool> SendEmailAsync(MailMessage message, MailAddress address);
    }
}