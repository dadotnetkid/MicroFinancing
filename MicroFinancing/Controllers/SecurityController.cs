using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MicroFinancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly SignInManager<ApplicationUser> _userManager;

        public SecurityController(ISecurityService securityService, SignInManager<ApplicationUser> userManager)
        {
            _securityService = securityService;
            _userManager = userManager;
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
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _userManager.SignOutAsync();
            return Redirect("/");
        }
    }
}
