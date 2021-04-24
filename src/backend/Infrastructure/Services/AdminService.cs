using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Models;
using Core.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        public UserManager<ApplicationUser> _userManager { get; set; }
        public AdminService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<BasicApiResponse<List<string>>> DeleteUser(string id)
        {
            var identityResult = await _userManager.DeleteAsync(new ApplicationUser() { Id = id });
            if (identityResult.Succeeded)
                return new BasicApiResponse<List<string>>() { Data = null, message = "User Deketed", flag = 0, isSuccess = true };
            return new BasicApiResponse<List<string>>() { Data = identityResult.Errors.Select(x => x.Description).ToList(), message = "Error Occured", flag = 1, isSuccess = false };
        }
        public async Task<BasicApiResponse<ApplicationUser>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new BasicApiResponse<ApplicationUser>()
                {
                    Data = null,
                    message = $"Didn't find user",
                    flag = 1,
                    isSuccess = false
                };
            return new BasicApiResponse<ApplicationUser>()
            {
                Data = user,
                message = $"Loaded users",
                flag = 0,
                isSuccess = true
            };
        }
        public BasicApiResponse<IQueryable<ApplicationUser>> GetUsers(int page)
        {
            var users = _userManager.Users.Skip((page - 1) * 10).Take(10);
            return new BasicApiResponse<IQueryable<ApplicationUser>>()
            {
                Data = users,
                message = $"Loaded users",
                flag = 0,
                isSuccess = true
            };
        }
    }
}