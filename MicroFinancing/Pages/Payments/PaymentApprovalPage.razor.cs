using MicroFinancing.Components.DialogComponent;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using Syncfusion.Blazor.Grids;

namespace MicroFinancing.Pages.Payments
{
    public partial class PaymentApprovalPage
    {
        [Inject] private IPaymentService paymentService { get; set; }
        [Inject] private IDialogService DialogService { get; set; }
        public SfGrid<PaymentForApprovalDto> PaymentApprovalGrid { get; set; }

        private void OnApproved(PaymentsForApprovalByDateDto item)
        {
            DialogService.ShowDialog("Approval Message", "Do you want to approved this payment",
                                    async (e) =>
                                    {
                                        if (!e)
                                        {
                                            return;
                                        }

                                        await paymentService.PaymentApproval(item);
                                        await PaymentApprovalGrid.Refresh();
                                    });
        }
    }
}
