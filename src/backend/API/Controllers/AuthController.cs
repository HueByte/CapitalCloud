using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Authentication;
using API.Extensions;
using Common.ApiResponse;
using Common.lib.ApiResponse;
using Core.DTOModels;
using Core.Entities;
using Core.Models;
using Core.RepositoriesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace API.Controllers
{
    // [Route("api/[controller]")]
    // [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        private readonly IJwtAuthentication _jwtAuth;
        public AuthController(UserManager<ApplicationUser> userManager, IUserService userService, IJwtAuthentication jwtAuth)
        {
            _userService = userService;
            _userManager = userManager;
            _jwtAuth = jwtAuth;
        }

        /// <summary>
        ///Register new User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /register
        ///     {
        ///        "username": "joseph",
        ///        "email": "joseph@spam.com"
        ///        "password" : "JosephRulez123"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Created new user</response>
        /// <response code="400">Error Occured during adding user</response>  
        [HttpPost("Register")]
        [ProducesResponseType(typeof(RegisterResponse), 400)]
        [ProducesResponseType(typeof(RegisterResponse), 200)]

        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO registerModel)
        {

            var response = await _userService.RegisterUserAsync(registerModel);
            if (response.isSuccess)
                return Ok(response);
            else
                return BadRequest(response);
        }

        /// <summary>
        ///Login User
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /login
        ///      {
        ///        "email": "joseph@spam.com"
        ///        "password" : "JosephRulez123"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">User Loged in</response>
        /// <response code="400">Error Occured during Loging. Returns Empty Object</response>  
        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginResponse), 200)]
        [ProducesResponseType(typeof(LoginResponse), 400)]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO loginModel)
        {
            var response = await _userService.LoginUserAsync(loginModel);
            if (!String.IsNullOrEmpty(response.token))
                return Ok(response);
            return BadRequest(response);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([FromQuery(Name = "token")] string token)
        {
            var response = await _userService.ConfirmEmail(token);
            if (response.isSuccess) return Ok(response.message);
            else return BadRequest(response.Data);
        }

        [HttpPost("FetchNewData")]
        [Authorize]
        public async Task<IActionResult> FetchNewUserDataAsync([FromBody] string token)
        {
            Console.WriteLine(token);
            try
            {
                string email = _jwtAuth.GetEmailFromToken(token);
                var newUserData = await _userService.FetchNewUserDataAsync(email);
                return Ok(newUserData);
            }
            catch (Exception e) { Console.WriteLine(e); }

            return Ok();
        }
    }
}