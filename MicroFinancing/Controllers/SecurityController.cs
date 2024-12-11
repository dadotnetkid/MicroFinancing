using System.Net.WebSockets;

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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SecurityController(ISecurityService securityService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _securityService = securityService;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        [HttpPost("login")]
        public async Task<ActionResult<BaseResultDto<RefreshToken>>> Login([FromBody] SecurityDto.LoginModel loginModel)
        {
            var res = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, true, false);

            if (!res.Succeeded)
            {
                return Ok(BaseResultDto<bool>.Fail(res.ToString()));
            }

            var refreshToken = await _securityService.CreateRefreshToken(loginModel.UserName);

            return Ok(BaseResultDto<RefreshToken>.Success(refreshToken));
        }
        [HttpPost]
        public async Task<ActionResult<BaseResultDto<RefreshToken>>> ReLogin([FromBody] string refreshToken)
        {
            var res = await _securityService.RefreshToken(refreshToken);

            if (res == null)
            {
                return Ok(BaseResultDto<RefreshToken>.Fail("Invalid Refresh Token"));
            }

            var user = await _userManager.FindByIdAsync(res.UserId);

            await _signInManager.SignInAsync(user, true);

            return Ok(BaseResultDto<RefreshToken>.Success(res));
        }
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Redirect("/");
        }
    }
}
