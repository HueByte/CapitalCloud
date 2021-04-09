using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.ApiResponse;
using Common.lib.ApiResponse;
using Core.DTOModels;
using Core.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

namespace API.Authentication
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtAuthentication _jwtAuthentication;
        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IJwtAuthentication jwtAuthentication)
        {
            _jwtAuthentication = jwtAuthentication;
            _roleManager = roleManager;
            _userManager = userManager;

        }

        public async Task<LoginResponse> LoginUserAsync(LoginDTO loginModel)
        {
            // Find user by mail. Return Service Response with empty LoginResponse if don't find
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null) return new LoginResponse() { Errors = new List<string>() { "User not found" } };
            // Verify User Password. Return Service Response with empty LoginResponse if password wrong
            var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!result) return new LoginResponse() { Errors = new List<string>() { "User not found" } }; ;
            //Generate token and return Service Response with Token in LoginResponse
            var userToken = _jwtAuthentication.GenerateJsonWebToken(user);
            return new LoginResponse
            {
                isSuccess = true,
                token = userToken,
                UserName = user.UserName,
                level = user.lvl,
                avatar_url = user.Avatar_Url,
                exp = user.exp,
                Email = user.Email,
                tokenType = "Bearer",
                expiresDate = DateTime.Now.AddDays(30)
            };
        }

        public async Task<RegisterResponse> RegisterUserAsync(RegisterDTO registermodel)
        {
            if (registermodel == null)
                return new RegisterResponse() { Errors = new List<string>() { "EmptyForm" } };
            var user = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = registermodel.Username,
                Email = registermodel.Email,
                Roles = new List<string>() { "user" }
            };
            
            var result = await _userManager.CreateAsync(user, registermodel.Password);
            if (result.Succeeded)
            {
                return new RegisterResponse() { isSuccess = true };
            }
            else
            {
                var errorlist = result.Errors.Select(e => e.Description);
                return new RegisterResponse() { Errors = errorlist.ToList() };
            }
        }
    }
}