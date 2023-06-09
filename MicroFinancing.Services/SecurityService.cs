using System.IdentityModel.Tokens.Jwt;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using MicroFinancing.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;


namespace MicroFinancing.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtSetting _jwtSetting;

        public SecurityService(IOptions<JwtSetting> options, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtSetting = options.Value;
        }
        public async Task<object?> CreateToken(SecurityDTM.LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.UserName);
            var result = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if (result)
            {

                var key = Encoding.ASCII.GetBytes
                    (_jwtSetting.Key);
                var claims = new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, loginModel.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, loginModel.Password),
                    new Claim(JwtRegisteredClaimNames.Jti, user.Id),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)

                };
                var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddHours(360),
                    Issuer = _jwtSetting.Issuer,
                    Audience = _jwtSetting.Audience,
                    SigningCredentials = signingCredentials
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var stringToken = tokenHandler.WriteToken(token);

                var res = new
                {
                    userName = loginModel.UserName,
                    Id = user.Id,
                    stringToken = stringToken,
                    dateTo = token.ValidTo,
                    fullName = user.FirstName + " " + user.LastName,
                };
                return res;
            }
            return null;
        }
    }
}
