using MicroFinancing.Components.DialogComponent;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;

using Microsoft.AspNetCore.Components;

using Syncfusion.Blazor.Grids;

namespace MicroFinancing.Pages.Lendings.LendingForApproval;

public partial class Index
{
    [Inject] ILendingService LendingService { get; set; }
    [Inject] IDialogService DialogService { get; set; }
    public SfGrid<LendingForApprovalGridDTM> LendingGridRef { get; set; }
    public IEnumerable<LendingForApprovalGridDTM> LoanForApprovals { get; set; } = [];
    public AddLendingForApproval AddLendingRef { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await GridReload();

            StateHasChanged();
        }
    }

    private async Task GridReload()
    {
        LoanForApprovals = await LendingService.GetLendingNotApproved();

        await LendingGridRef.Refresh();
    }

    private void AddLoan()
    {
        AddLendingRef.Show();
    }

    private async Task OnSubmitSuccess(bool obj)
    {
        await GridReload();
    }

    private async Task Release(LendingForApprovalGridDTM? item)
    {
        var res = await DialogService.ShowDialog("Release", "Do you want to release this item");

        if (!res)
        {
            return;
        }

        await LendingService.Release(item);

        await GridReload();
    }
}
