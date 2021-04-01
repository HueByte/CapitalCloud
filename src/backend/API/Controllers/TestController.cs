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
            _repository.InsertOne(new TestEntity(){
                Id = new Guid(),
                Name = "Testowy",
                Email = "test@test",
                Password = "123"
            });
            return Ok();
        }
    }
}