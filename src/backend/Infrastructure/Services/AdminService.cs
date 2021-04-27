using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Models;
using Core.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private UserManager<ApplicationUser> _userManager { get; set; }
        private RoleManager<ApplicationRole> _roleManager;
        public AdminService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<BasicApiResponse<List<string>>> DeleteUser(string id)
        {
            var identityResult = await _userManager.DeleteAsync(new ApplicationUser() { Id = id });
            if (!identityResult.Succeeded)
                return new BasicApiResponse<List<string>>() { Data = identityResult.Errors.Select(x => x.Description).ToList(), message = "Error Occured", flag = 1, isSuccess = false };
            Log.Information($"Delete user: {id}");
            return new BasicApiResponse<List<string>>() { Data = null, message = "User Deketed", flag = 0, isSuccess = true };
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

        public async Task<BasicApiResponse<List<string>>> GrantRole(string userId, string rolename)
        {
            var role = await _roleManager.FindByNameAsync(rolename);
            if (role == null)
                return StaticResponse<List<string>>.BadResponse(null, "No role finded", 1);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return StaticResponse<List<string>>.BadResponse(null, "No user finded", 1);

            var result = await _userManager.AddToRoleAsync(user, "admin");
            if (!result.Succeeded)
                return StaticResponse<List<string>>.BadResponse(result.Errors.Select(x => x.Description).ToList(), "Error Occured", 1);

            Log.Information($"Granted {role.Name} for {user.UserName}");

            return StaticResponse<List<string>>.GoodResponse(null, "Role Granted!");
        }
        
        public async Task<BasicApiResponse<List<string>>> RevokeRole(string userId, string rolename)
        {
            var role = await _roleManager.FindByNameAsync(rolename);
            if (role == null)
                return StaticResponse<List<string>>.BadResponse(null, "No role finded", 1);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return StaticResponse<List<string>>.BadResponse(null, "No user finded", 1);

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (!result.Succeeded)
                return StaticResponse<List<string>>.BadResponse(result.Errors.Select(x => x.Description).ToList(), "Error Occured", 1);

            Log.Information($"Revoke {role.Name} from {user.UserName}");

            return StaticResponse<List<string>>.GoodResponse(null, "Role Granted!");
        }

        public async Task<BasicApiResponse<List<string>>> ConfirmUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            if (user == null)
                return StaticResponse<List<string>>.BadResponse(null, "No user finded", 1);

            if (await _userManager.IsEmailConfirmedAsync(user))
                return StaticResponse<List<string>>.BadResponse(null, "User is already Confirmed", 1);

            user.EmailConfirmed = true;
            await _userManager.UpdateAsync(user);
            
            Log.Information($"Confirmed {user} with AdminPanel");

            return StaticResponse<List<string>>.GoodResponse(null, "User Confirmed!");
        }
    }
}