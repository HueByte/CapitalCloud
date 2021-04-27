using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Configuration;
using API.Extensions;
using Core.Entities;
using Core.Models;
using Core.RepositoriesInterfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace API.Controllers
{

    public class TestController : BaseApiController
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public TestController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;

        }

        /// <summary>
        /// Api For Testing
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Get()
        {
            var role = await _roleManager.FindByNameAsync("admin");
            var user = await _userManager.FindByIdAsync("337dc7bc-29cd-4402-94f4-e14868e7cfe8");
            await _userManager.AddToRoleAsync(user,role.Name);

            return Redirect("google.com");
        }

        [HttpGet("Tester")]
        public IActionResult Test()
        {
            return Ok("Welcome");
        }
    }
}