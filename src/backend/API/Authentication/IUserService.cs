using System.Threading.Tasks;
using Core.DTOModels;
using Core.Models;

namespace API.Authentication
{
    public interface IUserService
    {
         Task<ServiceResponse<string>> RegisterUserAsync(RegisterDTO registerModel);

         Task<ServiceResponse<LoginResponse>> LoginUserAsync(LoginDTO loginModel);
    }
}