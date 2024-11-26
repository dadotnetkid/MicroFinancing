using MicroFinancing.Interfaces.Services;

using Syncfusion.Blazor;

namespace MicroFinancing.Services;

public sealed class PaymentApprovalAdaptor : DataAdaptor
{
    private readonly IPaymentService _paymentService;

    public PaymentApprovalAdaptor(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    public override async Task<object> ReadAsync(DataManagerRequest dm, string? key = null)
    {

        try
        {
            return await _paymentService.GetPaymentForApproval(dm);
        }
        catch (Exception e)
        {
            throw;
        }
    }
}
