using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Authentication;
using Core.DTOModels;
using Core.Entities;
using Core.Models;
using Core.RepositoriesInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        public AuthController(UserManager<ApplicationUser> userManager, IUserService userService)
        {
            _userService = userService;
            _userManager = userManager;
        }
        /// <summary>
        ///pi For Testing A
        /// </summary>
        [HttpPost("Register")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterDTO registerModel)
        {
            var response = await _userService.RegisterUserAsync(registerModel);
            if (response.isSuccess)
                return Ok(response);
            else
                return BadRequest(response.message);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDTO loginModel)
        {
            var response = await _userService.LoginUserAsync(loginModel);
            if (response.isSuccess)
                return Ok(response.Data);
            else
                return BadRequest(response);
        }
    }
}