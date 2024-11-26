using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Syncfusion.Blazor.Data;

namespace MicroFinancing.Pages.Customers.Details;

public partial class ChangePayment
{
    private bool visibility;
    private Query query = new();
    public long LendingId { get; set; }
    public long PaymentId { get; set; }

    [Inject] private IPaymentService paymentService { get; set; }
    public void Show(PaymentGridDTM? payment)
    {
        visibility = true;
        PaymentId = payment.Id;
        query.AddParams("CustomerId", payment.CustomerId);
        StateHasChanged();
    }

    private async Task OnBtnSubmitClick(MouseEventArgs obj)
    {

        await paymentService.ChangePayment(PaymentId, LendingId);

        Hide();
    }

    private void OnBtnCancelClick(MouseEventArgs obj)
    {
        Hide();
    }

    private void Hide()
    {
        visibility = false;
        StateHasChanged();
    }
}
