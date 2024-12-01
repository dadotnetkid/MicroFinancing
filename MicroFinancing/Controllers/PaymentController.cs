using System.Text.Json;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Services;
using MicroFinancing.WebAssembly.Common;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Blazor;

namespace MicroFinancing.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost(nameof(GetPaymentForApproval))]
    public async Task<ActionResult<DataResultDto<PaymentForApprovalDto>>> GetPaymentForApproval(
        [FromBody] string payload)
    {
        var dm = JsonSerializer.Deserialize<DataManagerRequest>(payload);

        var payment = (await _paymentService.GetPaymentForApproval(dm))
            .ToDataResultDto<PaymentForApprovalDto>();

        return Ok(payment);
    }

    [HttpPost(nameof(PaymentApproval))]
    public async Task<ActionResult<BaseResultDto<bool>>> PaymentApproval(
        [FromBody] PaymentsForApprovalByDateDto payload)
    {
        try
        {
            await _paymentService.PaymentApproval(payload);

            return Ok(BaseResultDto<bool>.Success(true));
        }
        catch (Exception e)
        {
            return Ok(BaseResultDto<bool>.Fail(e.Message));
        }
    }
}