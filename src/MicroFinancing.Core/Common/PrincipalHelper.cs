using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MicroFinancing.Core.Common;

public static class PrincipalHelper
{
    public static string? GetUserFullName(this ClaimsPrincipal principal)
    {
        return principal.FindFirst("FullName")
                        ?.Value;
    }

    public static string? GetUserId(this ClaimsPrincipal principal)
    {
        return principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)
                        ?.Value;
    }

    public static bool IsAuthorized(this ClaimsPrincipal principal,
                                    string policy)
    {
        var authorization = DependencyHelper.GetRequiredService<IAuthorizationService>();

        var result = authorization.AuthorizeAsync(principal, policy)
                                  .ConfigureAwait(false)
                                  .GetAwaiter()
                                  .GetResult();

        return result.Succeeded;
    }

    public static bool IsAuthorized(this ClaimsPrincipal principal,
                                    params string[] policies)
    {
        var authorization = DependencyHelper.GetRequiredService<IAuthorizationService>();

        foreach (var policy in policies)
        {
            var result = authorization.AuthorizeAsync(principal, policy)
                                      .ConfigureAwait(false)
                                      .GetAwaiter()
                                      .GetResult();

            if (result.Succeeded)
            {
                return true;
            }
        }

        return false;
    }

    public static async Task<bool> IsAuthorizedAsync(this ClaimsPrincipal principal,
                                                     params string[] policies)
    {
        var authorization = DependencyHelper.GetRequiredService<IAuthorizationService>();

        foreach (var policy in policies)
        {
            var result = await authorization.AuthorizeAsync(principal, policy);

            if (result.Succeeded)
            {
                return true;
            }
        }

        return false;
    }
}
