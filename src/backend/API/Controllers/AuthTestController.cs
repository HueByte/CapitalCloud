using API.Extensions;
using Core.Models;
using Common.lib.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthTestController : BaseApiController
    {
        public AuthTestController() { }

        [Authorize(Roles = Roles.User)]
        [HttpGet("VerifyUser")]
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        public IActionResult VerifyUser() => Ok(new ServiceResponse<string>
        {
            Data = "Verified",
            isSuccess = true,
        });

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("VerifyAdmin")]
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        public IActionResult VerifyAdmin() => Ok(new ServiceResponse<string>
        {
            Data = "Verified",
            isSuccess = true
        });

        [Authorize(Roles = Roles.Admin + "," + Roles.User)]
        [HttpGet("VerifyAdminUser")]
        [ProducesResponseType(typeof(ServiceResponse<string>), 200)]
        public IActionResult VerifyAdminUser() => Ok(new ServiceResponse<string>
        {
            Data = "Verified",
            isSuccess = true
        });
    }
}