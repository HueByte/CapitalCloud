using API.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ConnectionTestController : BaseApiController
    {
        [HttpGet("Alive")]
        public IActionResult IsAlive() => Ok();
    }
}