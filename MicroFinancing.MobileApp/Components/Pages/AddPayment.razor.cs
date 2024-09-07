using MicroFinancing.MobileApp.Services.Client;

using Microsoft.AspNetCore.Components;

namespace MicroFinancing.MobileApp.Components.Pages;

public partial class AddPayment
{
    [Parameter] public long CustomerId { get; set; }
    [Inject] private IPaymentClient PaymentClient { get; set; }
    private CreatePaymentDTM model = new();
}
