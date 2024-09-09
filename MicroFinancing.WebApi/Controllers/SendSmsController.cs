using MicroFinancing.DataTransferModel;
using MicroFinancing.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroFinancing.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SendSmsController : ControllerBase
{
    private readonly ISmsService _smsService;

    public SendSmsController(ISmsService smsService)
    {
        _smsService = smsService;
    }

    [HttpPost]
    public IActionResult SendSms([FromBody] SendSmsRequestPayload payload)
    {
        _smsService.SendSms(payload.PhoneNumber, payload.Message);

        return Ok();
    }
}