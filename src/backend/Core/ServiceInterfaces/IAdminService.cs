using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Models;

namespace Core.ServiceInterfaces
{
    public interface IAdminService
    {
        BasicApiResponse<IQueryable<ApplicationUser>> GetUsers(int page);
        Task<BasicApiResponse<ApplicationUser>> GetUserById(string id);
        Task<BasicApiResponse<List<string>>> DeleteUser(string id);
    }
}