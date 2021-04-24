using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using Common.lib.Types;
using Core.Entities;
using Core.Models;
using Core.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpGet("users/{page}")]
        public IActionResult GetUsers(int page)
        {
            var response = _adminService.GetUsers(page);
            return Ok(response);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var response = await _adminService.GetUserById(id);
            if (response.isSuccess) return Ok(response);
            return BadRequest(response);
        }
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteuserById(string id)
        {
            var response = await _adminService.DeleteUser(id);
            if (response.isSuccess) return Ok(response);
            return BadRequest(response);
        }
    }
}