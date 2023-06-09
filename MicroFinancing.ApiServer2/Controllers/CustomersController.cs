using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MicroFinancing.ApiServer2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {

        [Authorize]
        [HttpGet("GetCustomers")]
        public IActionResult GetCustomers()
        {
            return Ok();
        }

    }
}
