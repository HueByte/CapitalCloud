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
                return new BasicApiResponse<List<string>>() { Data = identityResult.Errors.Select(x => x.Description).ToList(), errors = new List<string>() { "Request failed" }, flag = 1, isSuccess = false };

            Log.Information($"Deleted user: {id}");
            return new BasicApiResponse<List<string>>() { Data = null, errors = null, flag = 0, isSuccess = true };
        }
        public async Task<BasicApiResponse<ApplicationUser>> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return new BasicApiResponse<ApplicationUser>()
                {
                    Data = null,
                    errors = new List<string>() { "Didn't find user" },
                    flag = 1,
                    isSuccess = false
                };
            return new BasicApiResponse<ApplicationUser>()
            {
                Data = user,
                errors = null,
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
                errors = null,
                flag = 0,
                isSuccess = true
            };
        }

        public async Task<BasicApiResponse<List<string>>> GrantRole(string userId, string rolename)
        {
            var role = await _roleManager.FindByNameAsync(rolename);
            if (role == null)
                return new BasicApiResponse<List<string>>(null, new List<string>() { "Role not found" }, false, 1);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new BasicApiResponse<List<string>>(null, new List<string>() { "User not found" }, false, 1);

            var result = await _userManager.AddToRoleAsync(user, role.Name);
            if (!result.Succeeded)
                return new BasicApiResponse<List<string>>(null, new List<string>() { "Couldn't add user to role" }, false, 1);

            Log.Information($"Granted {role.Name} for {user.UserName}");

            return new BasicApiResponse<List<string>>(null, null, true, 0);

            // var role = await _roleManager.FindByNameAsync(rolename);
            // if (role == null)
            //     return StaticResponse<List<string>>.BadResponse(null, "No role found", 1);

            // var user = await _userManager.FindByIdAsync(userId);
            // if (user == null)
            //     return StaticResponse<List<string>>.BadResponse(null, "No user found", 1);

            // var result = await _userManager.AddToRoleAsync(user, "admin");
            // if (!result.Succeeded)
            //     return StaticResponse<List<string>>.BadResponse(result.Errors.Select(x => x.Description).ToList(), "Error Occured", 1);

            // Log.Information($"Granted {role.Name} for {user.UserName}");

            // return StaticResponse<List<string>>.GoodResponse(null, "Role Granted!");
        }

        public async Task<BasicApiResponse<List<string>>> RevokeRole(string userId, string rolename)
        {
            var role = await _roleManager.FindByNameAsync(rolename);
            if (role == null)
                return new BasicApiResponse<List<string>>(null, new List<string>() { "Role not found" }, false, 1);

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return new BasicApiResponse<List<string>>(null, new List<string>() { "User not found" }, false, 1);

            var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            if (!result.Succeeded)
                return new BasicApiResponse<List<string>>(null, new List<string>() { "Couldn't remove user from that role" }, false, 1);

            Log.Information($"Removed {user.UserName} from {role.Name} role");

            return new BasicApiResponse<List<string>>(null, null, true, 0);

            // var role = await _roleManager.FindByNameAsync(rolename);
            // if (role == null)
            //     return StaticResponse<List<string>>.BadResponse(null, "No role finded", 1);

            // var user = await _userManager.FindByIdAsync(userId);
            // if (user == null)
            //     return StaticResponse<List<string>>.BadResponse(null, "No user finded", 1);

            // var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
            // if (!result.Succeeded)
            //     return StaticResponse<List<string>>.BadResponse(result.Errors.Select(x => x.Description).ToList(), "Error Occured", 1);

            // Log.Information($"Revoke {role.Name} from {user.UserName}");

            // return StaticResponse<List<string>>.GoodResponse(null, "Role Granted!");
        }

        public async Task<BasicApiResponse<List<string>>> ConfirmUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                // return StaticResponse<List<string>>.BadResponse(null, "No user finded", 1);
                return new BasicApiResponse<List<string>>(null, new List<string>() { "User not found" }, false, 1);

            if (await _userManager.IsEmailConfirmedAsync(user))
                // return StaticResponse<List<string>>.BadResponse(null, "User is already Confirmed", 1);
                return new BasicApiResponse<List<string>>(null, new List<string>() { "User email is already confirmed" }, false, 1);

            user.EmailConfirmed = true;

            await _userManager.UpdateAsync(user);

            // Explain ?
            // Log.Information($"Confirmed {user} with AdminPanel");

            // return StaticResponse<List<string>>.GoodResponse(null, "User Confirmed!");
            return new BasicApiResponse<List<string>>(null, null, true, 0);
        }
    }
}