using System.Security.Claims;
using MicroFinancing.Core.Common;
using MicroFinancing.Interfaces.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MicroFinancing.Services;

public class BlazorCurrentUser : ICurrentUser
{
    private readonly IServiceScopeFactory _factory;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public BlazorCurrentUser(IServiceScopeFactory factory, AuthenticationStateProvider authenticationStateProvider)
    {
        _factory = factory;
        _authenticationStateProvider = authenticationStateProvider;
    }



    public ClaimsPrincipal User => _authenticationStateProvider.GetAuthenticationStateAsync().GetAwaiter().GetResult().User;

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