using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MicroFinancing.ApiServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSetting _jwtSetting;

        /*public SecurityController(IOptions<JwtSetting> options, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSetting = options.Value;
        }*/
        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SecurityDTM.LoginModel loginModel)
        {
            
            return Ok(await _securityService.CreateToken(loginModel));

        }
    }
}
