using System.Threading.Tasks;
using Core.Entities;

namespace Core.ServiceInterfaces
{
    public interface IEmailSender
    {
        Task SendActivationEmail(string email, string url);
    }
}