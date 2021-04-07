using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Models;

namespace Core.RepositoriesInterfaces
{
    public interface IUserManagmentService
    {
         Task<ServiceResponse<List<string>>> GetUsersUrlById(List<string> idList);
    }
}