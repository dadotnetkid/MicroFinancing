using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroFinancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;

        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SecurityDto.LoginModel loginModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var res = await _securityService.CreateToken(loginModel);
            if (res is null)
            {
                return BadRequest();
            }
            return Ok(res);
        }
    }
}
