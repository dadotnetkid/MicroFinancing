using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinancing.Core.Common
{
    public static class PrincipalHelper
    {

        public static string? GetUserFullName(this ClaimsPrincipal principal)
        {
            return principal.FindFirst("FullName")?.Value;
        }
        public static string? GetUserId(this ClaimsPrincipal principal)
        {
            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

    }
}
