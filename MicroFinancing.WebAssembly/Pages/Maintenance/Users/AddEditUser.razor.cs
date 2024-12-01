using Blazored.FluentValidation;

namespace MicroFinancing.WebAssembly.Pages.Maintenance.Users;

public partial class AddEditUser
{
    private FluentValidationValidator? fluentValidator;
    private CreateUpdateUserDTM user = new();
    private bool visibility;

    [CascadingParameter(Name = "MainPage")]
    private System.Index? MainPage { get; set; }

    [Inject] private IUserClient? userService { get; set; }
    [Inject] private IToasts toasts { get; set; }

    public void Show(CreateUpdateUserDTM? createUpdateUserDtm = null)
    {
        visibility = true;
        if (createUpdateUserDtm is not null) user = createUpdateUserDtm;
        StateHasChanged();
    }

    public void Hide()
    {
        visibility = false;
        user = new();
        StateHasChanged();
    }

    private void OnBtnCancelClick()
    {
        Hide();
    }

    private async Task OnBtnSubmitClick()
    {
        var validate = await fluentValidator?.ValidateAsync(x => { })!;
        if (validate)
        {
            if (string.IsNullOrEmpty(user.UserId))
            {
                var result = await userService.CreateUserAsync(user);

                user = result.Data;
            }
            else
            {
                await userService.UpdateUserAsync(user);
            }

            await userService.AddRolesAsync(user);
            // await MainPage?.RefreshGrid()!;
            //  await toasts.ShowToast("User", "Successfully execute action");
            Hide();
        }
    }
}