using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.RepositoriesInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMongoDbRepository<TestEntity> _repository;
        public TestController(IMongoDbRepository<TestEntity> repository)
        {
            _repository = repository;

        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}