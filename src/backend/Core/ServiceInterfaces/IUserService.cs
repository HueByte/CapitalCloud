using System.Threading.Tasks;
using Core.DTOModels;
using Core.Models;

namespace Core.ServiceInterfaces
{
    public interface IUserService
    {
         Task<ServiceResponse<RegisterDTO>> RegisterUserAsync(RegisterDTO registerModel);

         Task<ServiceResponse<LoginResponse>> LoginUserAsync(LoginDTO loginModel);
    }
}