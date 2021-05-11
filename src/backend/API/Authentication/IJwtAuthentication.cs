using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Core.Entities;

namespace API.Authentication
{
    public interface IJwtAuthentication
    {
         Task<string> GenerateJsonWebToken(ApplicationUser user);
         string GetEmailFromToken(string token);
    }
}