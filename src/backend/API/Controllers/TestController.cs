using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Models;
using Core.RepositoriesInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        int i = 0;
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
          var x = await _repository.DeleteById("9b2a4a45-d5f4-4d62-b853-117623799e8a");     
          return Ok(x);
        }
    }
}