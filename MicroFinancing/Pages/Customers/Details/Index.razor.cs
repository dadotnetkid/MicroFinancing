using MicroFinancing.Components.DialogComponent;
using MicroFinancing.Components.ToastsComponent;
using MicroFinancing.Core.Common;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;
using MicroFinancing.Services.Handlers;

using Microfinancing.WebApiClient.Services.Client;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Syncfusion.Blazor.SplitButtons;

using CustomerDetailDTM = MicroFinancing.DataTransferModel.CustomerDetailDTM;

namespace MicroFinancing.Pages.Customers.Details;

public partial class Index
{
    [Inject] ICustomerService customerService { get; set; }
    [Inject] NavigationManager navigationManager { get; set; }
    [Inject] IAuthorizationService authorizationService { get; set; }
    [Inject] IToasts toast { get; set; }
    [Inject] IUserService userService { get; set; }
    [Parameter] public long Id { get; set; }
    [Inject] IDialogService DialogService { get; set; }
    [Inject] ReConstructHandler ReConstructHandler { get; set; }
    [Inject] ICustomersClient CustomersClient { get; set; }
    private Payments? paymentRef;
    private AddPayment? addPaymentRef;
    private Lending? lendingRef;
    private AddLending? addLendingRef;
    private SetFlagDialog? setFlagRef;
    private PdfViewModal printPreview;
    private CustomerDetailDTM? model;
    public bool IsPrinting { get; set; }

    public BaseAuthorizePermissionDTM Permission { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await GetDetails();
        await GetPermission();
    }

    private async Task GetPermission()
    {
        Permission = await customerService.GetPermission();
    }

    public async Task GetDetails()
    { 
        model = await customerService.GetCustomerDetail(Id);

        StateHasChanged();
    }

    private async Task OnAddClick()
    {
        var res = await userService.IsAuthorizeAsync(ClaimsConstant.Customer.AddPayment);

        if (!res)
        {
            return;
        }

        addPaymentRef?.Show();
    }


    private async Task OnAddLendingClick()
    {
        var res = await userService.IsAuthorizeAsync(ClaimsConstant.Customer.AddLoan);
        if (!res) return;
        addLendingRef.Show();
    }

    private async Task SetFlag()
    {
        var res = await userService.IsAuthorizeAsync(ClaimsConstant.Customer.SetFlag);
        if (!res) return;
        setFlagRef.Show();
    }

    private async Task OnDropdownSelect(MenuEventArgs obj)
    {
        if (obj.Item.Id == GenericDropdownItem.PrintReport.ToString())
        {
            var res = await userService.IsAuthorizeAsync(ClaimsConstant.Customer.Print);
            if (!res) return;

            IsPrinting = true;
            await InvokeAsync(StateHasChanged);

            await Task.Factory.StartNew(async () =>
            {
                await printPreview.Open(Id);

                IsPrinting = false;
                await InvokeAsync(StateHasChanged);
            });
        }
    }

    private async Task OnRestructClick(MouseEventArgs obj)
    {

        var res = await DialogService.ShowDialog("Restruct",
                                                 "Do you want to restruct this customer");

        if (!res)
        {
            return;
        }

        await ReConstructHandler.Restruct(Id);
        await lendingRef.RefreshGrid();
    }
}
