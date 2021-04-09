using System.Threading.Tasks;
using Core.Entities;

namespace Core.ServiceInterfaces
{
    public interface IEmailSender
    {
         Task SendActivationEmail(ApplicationUser user);
    }
}