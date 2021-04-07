using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Configuration;
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        private readonly IMongoDbRepository<TestEntity> _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IUserManagmentService _userManagerService;
        public TestController(IMongoDbRepository<TestEntity> repository, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IUserManagmentService userManagerService)
        {
            _userManagerService = userManagerService;
            _roleManager = roleManager;
            _userManager = userManager;
            _repository = repository;

        }
        /// <summary>
        /// Api For Testing
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Get()
        {
            var x = await _userManagerService.GetUsersUrlById(new List<string>(){"bfb32a95-eac8-48f2-a049-90f96770420b"});

            return Ok(x);
        }
    }
}