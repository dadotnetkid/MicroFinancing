using System.ComponentModel;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace MicroFinancing.MobileApp.Providers;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _client;
    private readonly CookieContainer _container;
    private readonly NavigationManager _manager;
    private readonly ISecurityClient _securityClient;

    private const string REFRESH_TOKEN = "refresh-token";
    private const string COOKIES = "cookies";
    private const string COOKIE_NAME = ".AspNetCore.Identity.Application";

    public CustomAuthenticationStateProvider(HttpClient client, CookieContainer container, NavigationManager manager, ISecurityClient securityClient)
    {
        _client = client;
        _container = container;
        _manager = manager;
        _securityClient = securityClient;
    }
    public async Task Login(RefreshToken token)
    {
        await SecureStorage.SetAsync(REFRESH_TOKEN, token.Token);

        await SecureStorage.SetAsync(COOKIES,
                                     JsonSerializer.Serialize(_container.GetAllCookies()
                                                                        .FirstOrDefault(c => c.Name == ".AspNetCore.Identity.Application")));

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        _manager.NavigateTo("/");
    }

    public async Task Logout()
    {
        SecureStorage.Remove(REFRESH_TOKEN);
        SecureStorage.Remove(COOKIES);

        foreach (Cookie allCookie in _container.GetAllCookies())
        {
            allCookie.Expired = true;
        }

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();

        try
        {
            var userInfo = await SecureStorage.GetAsync(REFRESH_TOKEN);
            var cookies = await SecureStorage.GetAsync(COOKIES);

            if (_container.GetAllCookies()
                          .All(c => c.Name != COOKIE_NAME) && !string.IsNullOrEmpty(cookies))
            {
                _container.Add(JsonSerializer.Deserialize<Cookie>(cookies));
            }


            if (!string.IsNullOrEmpty(cookies))
            {
                var claims = new[] { new Claim(ClaimTypes.Name, "ffUser") };
                identity = new ClaimsIdentity(claims, "Server authentication");
            }
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Request failed:" + ex);
        }

        return new AuthenticationState(new ClaimsPrincipal(identity));
    }
}
