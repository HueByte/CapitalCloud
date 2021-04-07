using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Configuration;
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TestController : ControllerBase
    {
        private readonly LevelRepository _levelRepository;
        public TestController(LevelRepository levelRepository)
        {
            _levelRepository = levelRepository;


        }
        /// <summary>
        /// Api For Testing
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Get()
        {
            await _levelRepository.InsertOne(new LevelModel(){lvl=2,expToGrant=100});
            return Ok();
        }
    }
}