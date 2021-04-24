using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using Common.lib.Types;
using Core.Entities;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("users/{page}")]
        public IActionResult GetUsers(int page)
        {
            // TODO - Check query result for memory saving
            // var users = _userManager.Users.Skip((page - 1) * 10).Take(10);
            var users = _userManager.Users;
            return Ok(new BasicApiResponse<IQueryable<ApplicationUser>>()
            {
                Data = users.Skip((page - 1) * 10).Take(10),
                message = $"Loaded users",
                flag = 0,
                isSuccess = true
            });
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            if (user == null)
                return BadRequest(new BasicApiResponse<ApplicationUser>()
                {
                    Data = null,
                    message = $"Didn't find user",
                    flag = 1,
                    isSuccess = false
                });

            return Ok(new BasicApiResponse<ApplicationUser>()
            {
                Data = user,
                message = $"Loaded users",
                flag = 0,
                isSuccess = true
            });
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("user/{id}")]
        public async Task<IActionResult> DeleteuserById(string id)
        {
            var user = await _userManager.DeleteAsync(new ApplicationUser() { Id = id });
            return Ok();
        }
    }
}