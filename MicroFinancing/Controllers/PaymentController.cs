using System.Text.Json;

using MicroFinancing.Core.Common;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;

using Microsoft.AspNetCore.Mvc;

using Syncfusion.Blazor;

namespace MicroFinancing.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost(nameof(GetPaymentForApproval))]
    public async Task<ActionResult<DataResultDto<PaymentForApprovalDto>>> GetPaymentForApproval([FromBody] string payload)
    {
        var dm = JsonSerializer.Deserialize<DataManagerRequest>(payload);

        var payment = (await _paymentService.GetPaymentForApproval(dm))
            .ToDataResultDto<PaymentForApprovalDto>();

        return Ok(payment);
    }

    [HttpPost(nameof(PaymentApproval))]
    public async Task<ActionResult<BaseResultDto<bool>>> PaymentApproval([FromBody] PaymentsForApprovalByDateDto payload)
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

    [HttpPost]
    public async Task<ActionResult<BaseResultDto<long>>> AddPayment([FromBody] CreatePaymentDTM item)
    {
        var payment = await _paymentService.AddPayment(item);

        return Ok(BaseResultDto<long>.Success(payment.Id));
    }

    [HttpPost]
    public async Task<ActionResult<BaseResultDto<bool>>> UploadSignature([FromBody] UploadSignaturePayload payload)
    {
        await _paymentService.UploadFile(payload.UploadFiles, payload.PaymentId);

        return Ok(BaseResultDto<bool>.Success(true));
    }

    [HttpGet]
    public async Task<ActionResult<BaseResultDto<List<PaymentGridDTM>>>> GetPaymentByCollectorId()
    {
        var res = await _paymentService.GetPaymentByCollectorId(User.GetUserId());

        return Ok(BaseResultDto<List<PaymentGridDTM>>.Success(res));
    }
}
