using MicroFinancing.Components.DialogComponent;
using MicroFinancing.Components.ToastsComponent;
using MicroFinancing.Core.Common;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;

using Microsoft.AspNetCore.Components;

using Syncfusion.Blazor.SplitButtons;

namespace MicroFinancing.Pages.Customers.Details;

public partial class Lending
{
    private EditLending editLendingRef;
    private PdfViewModal printPreview;
    [Inject] IDialogService DialogService { get; set; }
    [Inject] IUserService userService { get; set; }
    [Inject] ILendingService lendingService { get; set; }

    private async Task OnDropdownSelectedMenu(MenuEventArgs menuEventArgs,
                                              LendingGridDTM context)
    {
        if (menuEventArgs.Item.Id == GenericDropdownItem.Delete.ToString())
        {
            await DeleteLending(context);
        }
        if (menuEventArgs.Item.Id == GenericDropdownItem.ViewDetails.ToString())
        {
            await EditLendingDetails(context);
        }
        if (menuEventArgs.Item.Id == GenericDropdownItem.SetActiveLoan.ToString())
        {
            await SetActiveLoan(context);
        }
        if (menuEventArgs.Item.Id == GenericDropdownItem.PreviewSOA.ToString())
        {
            await PreviewSOA(context);
        }
    }

    private async Task PreviewSOA(LendingGridDTM context)
    {
        var res = await userService.IsAuthorizeAsync(ClaimsConstant.Customer.Print);
        if (!res) return;

        IsPrinting = true;
        await InvokeAsync(StateHasChanged);

        await Task.Factory.StartNew(async () =>
        {
            await printPreview.PreviewSOAByLendingId(context.Id);

            IsPrinting = false;
            await InvokeAsync(StateHasChanged);
        });
    }

    public bool IsPrinting { get; set; }

    private async Task EditLendingDetails(LendingGridDTM context)
    {
        var authorize = await userService.IsAuthorizeAsync(ClaimsConstant.Customer.ManageLoan);

        if (!authorize)
        {
            return;
        }

        editLendingRef.Show(context.Id);
    }

    private async Task SetActiveLoan(LendingGridDTM context)
    {
        var authorize = await userService.IsAuthorizeAsync(ClaimsConstant.Customer.ManageLoan);

        if (!authorize)
        {
            return;
        }

        await lendingService.SetActiveLoan(context.Id);
        await lendingGrid.Refresh();
    }

    private async Task DeleteLending(LendingGridDTM context)
    {
        var authorize = await userService.IsAuthorizeAsync(ClaimsConstant.Customer.ManageLoan);

        if (!authorize)
        {
            return;
        }

        DialogService.ShowDialog("Delete",
                                 "Do you want to delete this item",
                                 async e =>
                                 {
                                     if (!e)
                                     {
                                         return;
                                     }
                                     await lendingService.DeleteLending(context.Id);
                                     await lendingGrid.Refresh();
                                 });
    }

    private async Task OnSubmitSuccess()
    {
        await lendingGrid.Refresh();
    }
}
