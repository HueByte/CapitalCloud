using System.Threading.Tasks;
using Common.ApiResponse;
using Common.lib.ApiResponse;
using Core.DTOModels;
using Core.Models;

namespace API.Authentication
{
    public interface IUserService
    {
         Task<ServiceResponse<RegisterResponse>> RegisterUserAsync(RegisterDTO registerModel);

         Task<ServiceResponse<LoginResponse>> LoginUserAsync(LoginDTO loginModel);
    }
}