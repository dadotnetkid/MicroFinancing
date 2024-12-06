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
    [Inject] ICustomersClient customersClient { get; set; }
    public BranchDto SelectedBranch { get; set; }

    IEnumerable<BranchDto> branches = new List<BranchDto>();
    public void Show(CreateUpdateUserDTM? createUpdateUserDtm = null)
    {

        visibility = true;

        if (createUpdateUserDtm is not null)
        {
            user = createUpdateUserDtm;

            SelectedBranch = branches.FirstOrDefault(x => x.Branch == user.Branch) ?? new BranchDto();
        }
        
        StateHasChanged();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var res = await customersClient.GetBranchesAsync();

            branches = res.Data;
        }
        await base.OnAfterRenderAsync(firstRender);
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
        user.Branch = SelectedBranch.Branch;

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