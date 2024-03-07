using System.Security.Claims;
using MicroFinancing.Core.Common;
using MicroFinancing.Interfaces.Services;
using Microsoft.AspNetCore.Components.Authorization;

namespace MicroFinancing.Services;

public class CurrentUser : ICurrentUser
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public CurrentUser(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    private AuthenticationState State => _authenticationStateProvider.GetAuthenticationStateAsync()
        .ConfigureAwait(false).GetAwaiter().GetResult();

    private ClaimsPrincipal User => State.User;


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