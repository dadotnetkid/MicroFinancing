using System.Text.Json;
using MicroFinancing.Interfaces.Services;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace MicroFinancing.Services;

public sealed class PaymentApprovalAdaptor : DataAdaptor
{
    private readonly IPaymentClient _paymentService;

    public PaymentApprovalAdaptor(IPaymentClient paymentService)
    {
        _paymentService = paymentService;
    }

    public override async Task<object> ReadAsync(DataManagerRequest dm, string? key = null)
    {
        try
        {
            var payload = JsonSerializer.Serialize(dm);
            var result = await _paymentService.GetPaymentForApprovalAsync(payload);

            return new DataResult()
            {
                Result = result.Result,
                Count = result.Count ?? 0
            };
        }
        catch (Exception e)
        {
            throw;
        }
    }
}