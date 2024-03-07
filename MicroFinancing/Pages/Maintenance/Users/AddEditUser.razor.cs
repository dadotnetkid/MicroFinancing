using Blazored.FluentValidation;
using MicroFinancing.Components.ToastsComponent;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace MicroFinancing.Pages.Maintenance.Users
{
    public partial class AddEditUser
    {
        [CascadingParameter(Name = "MainPage")] Index? MainPage { get; set; }
        private bool visibility;
        private FluentValidationValidator? fluentValidator;
        private CreateUpdateUserDTM user = new();
        [Inject] private IUserService? userService { get; set; }
        [Inject] private IToasts toasts { get; set; }
        public void Show(CreateUpdateUserDTM? createUpdateUserDtm = null)
        {
            visibility = true;
            if (createUpdateUserDtm is not null)
            {
                user = createUpdateUserDtm;
            }
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
                    user = await userService.CreateUser(user);
                }
                else
                {
                    await userService.UpdateUser(user);
                }
                await userService.AddRoles(user);
                await MainPage?.RefreshGrid()!;
              //  await toasts.ShowToast("User", "Successfully execute action");
                Hide();
            }
        }
    }
}
