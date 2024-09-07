using System.Security.Claims;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace MicroFinancing.MobileApp.Providers;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly HttpClient _client;
    private readonly NavigationManager _manager;

    public CustomAuthenticationStateProvider(HttpClient client, NavigationManager manager)
    {
        _client = client;
        _manager = manager;
    }
    public async Task Login(string token)
    {
        await SecureStorage.SetAsync("accounttoken", token);

        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        _manager.NavigateTo("/");
    }

    public async Task Logout()
    {
        SecureStorage.Remove("accounttoken");
        _client.DefaultRequestHeaders.Remove("Authorization");
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var identity = new ClaimsIdentity();

        try
        {
            var userInfo = await SecureStorage.GetAsync("accounttoken");
            if (!_client.DefaultRequestHeaders.Contains("Authorization") && !string.IsNullOrEmpty(userInfo))
            {
                _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {userInfo}");
            }



            if (userInfo != null)
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
