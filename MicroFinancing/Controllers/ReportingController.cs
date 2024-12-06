using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;
using MicroFinancing.Reporting;
using MicroFinancing.Services;
using MicroFinancing.Services.Handlers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroFinancing.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class ReportingController : ControllerBase
    {
        private readonly IReportingService _reportingService;
        private readonly ReportHandler _reportHandler;

        public ReportingController(IReportingService reportingService, ReportHandler reportHandler)
        {
            _reportingService = reportingService;
            _reportHandler = reportHandler;
        }
        [HttpGet("Download")]
        public async Task<IActionResult> Download(long customerId)
        {
            var report = new rptStatementofAccount()
            {
                DataSource = await _reportingService.StatementOfAccount(customerId)
            };
            using var ms = new MemoryStream();
            await report.ExportToPdfAsync(ms);
            return File(ms.ToArray(), "application/pdf", "SOA.pdf");
        }

        [HttpPost]
        public async Task<ActionResult<DataResultDto<PaymentSummaryDto>>> GeneratePaymentSummary([FromBody] BaseReportHandlerRequest? payload)
        {
            var result = await _reportHandler.Generate(payload);

            if (result is DataResultDto<PaymentSummaryDto> item)
            {
                return Ok(item);
            }

            return BadRequest();
        }
    }
}
