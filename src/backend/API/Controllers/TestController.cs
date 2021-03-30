using System;
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
        // private readonly IMongoDbRepository<TestEntity> _repository;
        public TestController()
        {
            // _repository = repository;

        }
        [HttpGet]
        public async Task<IActionResult> Gettttttttttt()
        {
            // await _repository.InsertOne(new TestEntity(){
            //     Id= new Guid(),
            //     Name = "abdul",
            //     Email = "abdul@abdul",
            //     Password = "123"
            // });
            return Ok();
        }
    }
}