using System.Security.Claims;
using MicroFinancing.Core.Common;
using MicroFinancing.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;

namespace MicroFinancing.WebAssembly.Services;

public class CurrentUser : ICurrentUser
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IAuthorizationService _authorizationService;

    public CurrentUser(AuthenticationStateProvider authenticationStateProvider,
        IAuthorizationService authorizationService)
    {
        _authenticationStateProvider = authenticationStateProvider;
        _authorizationService = authorizationService;
    }


    public ClaimsPrincipal User =>
        _authenticationStateProvider.GetAuthenticationStateAsync().GetAwaiter().GetResult().User;

    public bool IsInRole(string role)
    {
        return User.IsInRole(role);
    }

    public bool IsAuthorized(ClaimsPrincipal user, string policy)
    {
        var state = _authorizationService
            .AuthorizeAsync(user, policy)
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();

        return state.Succeeded;
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