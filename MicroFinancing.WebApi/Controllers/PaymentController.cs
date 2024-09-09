using MicroFinancing.Core.Common;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.Blazor.Inputs;

namespace MicroFinancing.WebApi.Controllers;

[Authorize]
[Route("api/[controller]/[action]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    // GET
    [HttpPost]
    public async Task<ActionResult<long>> AddPayment([FromBody] CreatePaymentDTM item)
    {
        var payment = await _paymentService.AddPayment(item);

        return Ok(payment.Id);
    }

    [HttpPost]
    public async Task<IActionResult> UploadSignature([FromBody] UploadSignaturePayload payload)
    {
        await _paymentService.UploadFile(payload.UploadFiles, payload.PaymentId);

        return Ok();
    }
}