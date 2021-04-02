using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.RepositoriesInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMongoDbRepository<TestEntity> _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        public TestController(IMongoDbRepository<TestEntity> repository, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _repository = repository;

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var x = await _userManager.CreateAsync(new ApplicationUser()
            {
                UserName = "joseph",
                
            });
            return Ok(x.Succeeded);
        }
    }
}