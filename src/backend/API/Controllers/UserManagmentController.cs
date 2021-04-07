using System.Collections.Generic;
using System.Threading.Tasks;
using Core.RepositoriesInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserManagmentController : ControllerBase
    {
        private readonly IUserManagmentService _userManagment;
        public UserManagmentController(IUserManagmentService userManagment)
        {
            _userManagment = userManagment;

        }
        [HttpGet("avatar")]
        public async Task<IActionResult> GetAvatar([FromBody]List<string> idList)
        {
            if(idList == null) return BadRequest("Incorrect Id");
            var response = await _userManagment.GetUsersUrlById(idList);
            if(!response.isSuccess)
            return BadRequest(response.message);
            else
            return Ok(response.Data);
        }

    }
}