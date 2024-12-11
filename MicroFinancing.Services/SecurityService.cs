using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MicroFinancing.Interfaces.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


namespace MicroFinancing.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<RefreshToken, bool> _refreshTokenRepository;
        private readonly JwtSetting _jwtSetting;

        public SecurityService(IOptions<JwtSetting> options, SignInManager<ApplicationUser> signInManager,
                               UserManager<ApplicationUser> userManager,
                               IRepository<RefreshToken, bool> refreshTokenRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
            _jwtSetting = options.Value;
        }
        public async Task<object?> CreateToken(SecurityDto.LoginModel loginModel)
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

        public async Task<RefreshToken> CreateRefreshToken(string userName)
        {
            var query = _refreshTokenRepository.Entity.Where(c => c.User.UserName == userName && c.Expires > DateTime.Now);

            if (query.Any())
            {
                return await query.FirstOrDefaultAsync();
            }

            var user = await _userManager.FindByNameAsync(userName);

            var refreshToken = new RefreshToken
            {
                UserId = user.Id,
                Expires = DateTime.Now.AddDays(7),
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)),
                CreatorUserId = user.Id
            };

            await _refreshTokenRepository.AddAsync(refreshToken);

            return refreshToken;
        }

        public async Task<RefreshToken?> RefreshToken(string refreshToken)
        {
            var token = _refreshTokenRepository.Entity.Where(c => c.Token == refreshToken && c.Expires < DateTime.Now);

            if (!token.Any())
            {
                return null;
            }

            var res = await token.FirstOrDefaultAsync();

            res.Expires = DateTime.Now.AddDays(7);

            await _refreshTokenRepository.SaveChangesAsync();

            return res;
        }
    }
}
