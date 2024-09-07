using System.Security.Claims;
using MicroFinancing.Core.Common;
using MicroFinancing.Interfaces.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MicroFinancing.Services;

public class CurrentUser : ICurrentUser
{
    private readonly IServiceScopeFactory _factory;

    public CurrentUser(IServiceScopeFactory factory)
    {
        _factory = factory;
    }

    private AuthenticationState State => _factory.CreateScope().ServiceProvider.GetRequiredService<AuthenticationStateProvider>().GetAuthenticationStateAsync().GetAwaiter().GetResult();

    private ClaimsPrincipal User
    {
        get
        {
            if (State is not null)
            {
                return State.User;
            }

            return _factory.CreateScope().ServiceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.User;
        }
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