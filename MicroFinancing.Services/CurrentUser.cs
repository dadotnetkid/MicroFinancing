using System.Security.Claims;

using MicroFinancing.Interfaces.Services;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MicroFinancing.Services;

public class CurrentUser : ICurrentUser
{
    private readonly HttpContext _httpContext;

    public CurrentUser(IHttpContextAccessor accessor)
    {
        _httpContext = accessor.HttpContext;
    }



    public ClaimsPrincipal User => _httpContext.User;

    public bool IsInRole(string role)
    {
        throw new NotImplementedException();
    }

    public bool IsAuthorized(ClaimsPrincipal user, string policy)
    {
        throw new NotImplementedException();
    }

    private string GetFullName()
    {
        return User.GetUserFullName();
    }

    private string GetUserId()
    {
        return User.GetUserId();
    }

    public string UserId => GetUserId();
    public string FullName => GetFullName();
}
