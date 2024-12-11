using Microsoft.AspNetCore.Components;

using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;

namespace MicroFinancing.MobileApp.Components.Pages.Customers;

public partial class Index
{
    [Inject] ICustomersClient CustomersClient { get; set; }
    [Inject] NavigationManager NavigationManager { get; set; }

    public IEnumerable<CustomerGridDTM> CustomersList { get; set; } = [];

    public string Search { get; set; }
    public SfGrid<CustomerGridDTM> customerGrid { get; set; }
    public Query Query { get; set; } = new();

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await LoadCustomers();
        }
    }

    private async Task LoadCustomers()
    {
        StateHasChanged();
    }

    private async Task OnSearch()
    {
        Query.Where("FullName", "contains", Search);

        await customerGrid?.Refresh();
    }

    private Task OnSelectedCustomer(CustomerGridDTM customerGridDtm)
    {
        NavigationManager.NavigateTo($"/AddPayment/{customerGridDtm.Id}");

        return Task.CompletedTask;
    }
}
