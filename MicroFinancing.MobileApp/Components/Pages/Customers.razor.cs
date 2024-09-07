using MicroFinancing.MobileApp.Services.Client;

using Microsoft.AspNetCore.Components;

namespace MicroFinancing.MobileApp.Components.Pages;

public partial class Customers
{
    [Inject] ICustomersClient CustomersClient { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }

    public IEnumerable<CustomerGridDTM> CustomersList { get; set; } = [];

    public string Search { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadCustomers();
        }
    }

    private async Task LoadCustomers()
    {
        this.CustomersList = await CustomersClient.GetCustomersAsync(Search);
        StateHasChanged();
    }

    private async Task OnSearch()
    {
        await LoadCustomers();
    }

    private Task OnSelectedCustomer(CustomerGridDTM customerGridDtm)
    {
        NavigationManager.NavigateTo($"/AddPayment/{customerGridDtm.Id}");

        return Task.CompletedTask;
    }
}
