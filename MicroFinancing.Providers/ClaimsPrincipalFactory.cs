using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MicroFinancing.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace MicroFinancing.Providers
{
    public sealed class ClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        public ClaimsPrincipalFactory(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, IOptions<IdentityOptions> options) :
            base(userManager, roleManager, options)
        {
        }
        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        {
            var claims = await base.GenerateClaimsAsync(user);
            claims.AddClaims(new[]
            {
                new Claim("FullName",$"{user?.FirstName } {user?.LastName }")
            });
            return claims;
        }
    }
}
