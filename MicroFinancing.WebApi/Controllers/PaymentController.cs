using MicroFinancing.Core.Common;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    [Authorize(Policy = ClaimsConstant.Customer.AddPayment)]
    public async Task<ActionResult<Payment>> AddPayment([FromBody] CreatePaymentDTM item)
    {
        var payment = await _paymentService.AddPayment(item);
        return Ok(payment);
    }
}