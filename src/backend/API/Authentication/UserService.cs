using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.ApiResponse;
using Common.lib.ApiResponse;
using Core.DTOModels;
using Core.Entities;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

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

        public async Task<ServiceResponse<LoginResponse>> LoginUserAsync(LoginDTO loginModel)
        {
            // Find user by mail. Return Service Response with empty LoginResponse if don't find
            var user = await _userManager.FindByEmailAsync(loginModel.Email);
            if (user == null) return StaticResponse<LoginResponse>.BadResponse(new LoginResponse(), "Didn't find user with this mail", 1);
            // Verify User Password. Return Service Response with empty LoginResponse if password wrong
            var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!result) return StaticResponse<LoginResponse>.BadResponse(new LoginResponse(), "Wrong password", 1);
            //Generate token and return Service Response with Token in LoginResponse
            var userToken = _jwtAuthentication.GenerateJsonWebToken(user);
            return new ServiceResponse<LoginResponse>(){
                Data = new LoginResponse{
                    token = userToken,
                    tokenType = "Bearer",
                    expiresDate = DateTime.Now.AddDays(30)
                },
                isSuccess = true,
                message = "Logged!",
                flag = 0
            };
            
            
        }

        public async Task<ServiceResponse<RegisterResponse>> RegisterUserAsync(RegisterDTO registermodel)
        {
            if (registermodel == null)
                return StaticResponse<RegisterResponse>.BadResponse(
                    new RegisterResponse()
                    {
                    Errors =  new List<string>(){"Empty Form"}
                    }, 
                "Empty form", 
                1);

            var user = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = registermodel.Username,
                Email = registermodel.Email
            };

            var result = await _userManager.CreateAsync(user, registermodel.Password);
            if (result.Succeeded)
            {
                return new ServiceResponse<RegisterResponse>(){
                    Data = new RegisterResponse(){isSuccess = true},
                    isSuccess = true,
                    message = " ok",
                     flag = 0};
            }
            else
            {
                var errorlist = result.Errors.Select(e => e.Description);
                return StaticResponse<RegisterResponse>.BadResponse(new RegisterResponse(){Errors=errorlist.ToList()}, "Error Occured", 1);
                //TODO - chagne this shit okay
            }

        }
    }
}