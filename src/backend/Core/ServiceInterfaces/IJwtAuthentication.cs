using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.ServiceInterfaces
{
    public interface IJwtAuthentication
    {
         string GenerateJsonWebToken(ApplicationUser user);
    }
}