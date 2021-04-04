using System.Threading.Tasks;
using Common.ApiResponse;
using Common.lib.ApiResponse;
using Core.DTOModels;
using Core.Models;

namespace API.Authentication
{
    public interface IUserService
    {
         Task<RegisterResponse> RegisterUserAsync(RegisterDTO registerModel);

         Task<LoginResponse> LoginUserAsync(LoginDTO loginModel);
    }
}