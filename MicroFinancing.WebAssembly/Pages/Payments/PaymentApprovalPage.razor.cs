using MicroFinancing.Components.DialogComponent;
using Syncfusion.Blazor.Grids;

namespace MicroFinancing.WebAssembly.Pages.Payments
{
    public partial class PaymentApprovalPage
    {
        [Inject] private IPaymentClient paymentService { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        public SfGrid<PaymentForApprovalDto> PaymentApprovalGrid { get; set; }

        private async Task OnApproved(PaymentsForApprovalByDateDto item)
        {
            var res = await DialogService.ShowDialog("Approval Message", "Do you want to approved this payment");

            if (!res)
            {
                return;
            }

            await paymentService.PaymentApprovalAsync(item);

            await PaymentApprovalGrid.Refresh();
        }
    }
}
