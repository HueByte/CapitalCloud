using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Core.Entities;

namespace API.Authentication
{
    public interface IJwtAuthentication
    {
         string GenerateJsonWebToken(ApplicationUser user);
    }
}