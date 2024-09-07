using MicroFinancing.MobileApp.Providers;
using MicroFinancing.MobileApp.Services.Client;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace MicroFinancing.MobileApp.Components.Pages;

public partial class Login
{
    [Inject] private IAccountClient AccountClient { get; set; }
    [Inject] private CustomAuthenticationStateProvider AuthenticationStateProvider { get; set; }

    public LoginModel LoginModel { get; set; } = new();
    private string ErrorMessage { get; set; }

    private async Task OnSubmit(EditContext obj)
    {
        try
        {
            var res = await AccountClient.LoginAsync(LoginModel);
            await AuthenticationStateProvider.Login(res);
        }
        catch (Exception e)
        {
            ErrorMessage = e.Message;
            StateHasChanged();
        }
    }
}
