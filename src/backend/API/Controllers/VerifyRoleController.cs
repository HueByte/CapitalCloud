using API.Extensions;
using Core.Models;
using Common.lib.Types;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using API.Authentication;

namespace API.Controllers
{
    public class VerifyRoleController : BaseApiController
    {
        public VerifyRoleController() { }

        [Authorize(Roles = Roles.User)]
        [HttpGet("VerifyUser")]
        [ProducesResponseType(typeof(BasicApiResponse<string>), 200)]
        public IActionResult VerifyUser() => Ok(new BasicApiResponse<string>
        {
            Data = "Verified",
            isSuccess = true,
        });

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("VerifyAdmin")]
        [ProducesResponseType(typeof(BasicApiResponse<string>), 200)]
        public IActionResult VerifyAdmin() => Ok(new BasicApiResponse<string>
        {
            Data = "Verified",
            isSuccess = true
        });

        [Authorize(Roles = Roles.Admin + "," + Roles.User)]
        [HttpGet("VerifyAdminUser")]
        [ProducesResponseType(typeof(BasicApiResponse<string>), 200)]
        public IActionResult VerifyAdminUser() => Ok(new BasicApiResponse<string>
        {
            Data = "Verified",
            isSuccess = true
        });

        [DynamicAuthorize]
        [HttpGet("RoleValidator")]
        public IActionResult RoleValidator() // validates routes 
        {
            return Ok();
        }
    }
}