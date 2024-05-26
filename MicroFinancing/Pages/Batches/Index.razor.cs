using MicroFinancing.Core.Common;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;
using MicroFinancing.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Grids;
using Syncfusion.Blazor.SplitButtons;

namespace MicroFinancing.Pages.Batches;

public sealed partial class Index
{
    private AddBatch addBatchRef;
    public SfGrid<BatchDto> BatchGrid { get; set; }

    [Inject] IUserService userService { get; set; }
    [Inject] NavigationManager navigationManager { get; set; }

    private void OnAddClick(MouseEventArgs arg)
    {
        addBatchRef.Show();
    }

    private async Task OnSuccess()
    {
        await BatchGrid.Refresh();
    }

    private async Task OnDropdownSelectedMenu(MenuEventArgs e, BatchDto context)
    {
        if (e.Item.Id == GenericDropdownItem.ViewDetails.ToString())
        {
            if (await userService.IsAuthorize(ClaimsConstant.Customer.View))
            {
                navigationManager.NavigateTo($"/Batch/{context.Id}");
            }
        }
    }
}