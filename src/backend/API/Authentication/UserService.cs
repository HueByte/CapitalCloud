using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.ApiResponse;
using Common.lib.ApiResponse;
using Core.DTOModels;
using Core.Entities;
using Core.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Core.RepositoriesInterfaces;

namespace API.Authentication
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IJwtAuthentication _jwtAuthentication;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IEmailConfirmationTokenRepository _emailRepo;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IJwtAuthentication jwtAuthentication,
        IEmailSender emailSender, IConfiguration configuration, IEmailConfirmationTokenRepository emailRepo)
        {
            _emailRepo = emailRepo;
            _configuration = configuration;
            _emailSender = emailSender;
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
            if (!result) return new LoginResponse() { Errors = new List<string>() { "Either e-mail or password is wrong" } }; ;
            //Generate token and return Service Response with Token in LoginResponse
            var userToken = _jwtAuthentication.GenerateJsonWebToken(user);
            return new LoginResponse
            {
                isSuccess = true,
                token = userToken,
                UserName = user.UserName,
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
                await SendConfirmEmail(user);
                return new RegisterResponse() { isSuccess = true };
            }
            else
            {
                var errorlist = result.Errors.Select(e => e.Description);
                return new RegisterResponse() { Errors = errorlist.ToList() };
            }
        }
        public async Task SendConfirmEmail(ApplicationUser user)
        {
            var dbResult = await _emailRepo.InsertOne(new EmailConfirmationToken() { 
                                userId = user.Id, 
                                token = await _userManager.GenerateEmailConfirmationTokenAsync(user), 
                                expiredAt = DateTime.Now.AddHours(24) });
                
            var url = _configuration.GetValue<string>("URL")
                      + "api/auth/emailconfirm?token="
                      + dbResult.Data.Id;
                      
            await _emailSender.SendActivationEmail(user.Email, url);
        }
    }
}