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
    [Authorize]
    public class TestController : BaseApiController
    {
        private readonly IMongoDbRepository<EmailConfirmationToken> _mongoDbRepository;

        public TestController(IMongoDbRepository<EmailConfirmationToken> mongoDbRepository)
        {
            _mongoDbRepository = mongoDbRepository;

        }

        /// <summary>
        /// Api For Testing
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Get()
        {
            await _mongoDbRepository.InsertOne(new EmailConfirmationToken(){
                userId = Guid.NewGuid().ToString(),
                token = "123",
                expiredAt = DateTime.Now.AddHours(24)
            });
            return Ok();
        }

        [HttpGet("Tester")]
        public IActionResult Test()
        {
            return Ok("Welcome");
        }
    }
}