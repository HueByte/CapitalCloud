using System.Collections.Generic;
using System.Threading.Tasks;
using API.Extensions;
using Core.RepositoriesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]

    public class UserManagmentController : BaseApiController
    {
        private readonly IUserManagmentService _userManagment;
        public UserManagmentController(IUserManagmentService userManagment)
        {
            _userManagment = userManagment;

        }
        
        [HttpGet("avatar")]
        public async Task<IActionResult> GetAvatar([FromBody] List<string> idList)
        {
            if (idList == null) return BadRequest("Incorrect Id");
            var response = await _userManagment.GetUsersUrlById(idList);
            if (!response.isSuccess)
                return BadRequest(response.message);
            else
                return Ok(response.Data);
        }

    }
}