using MicroFinancing.Interfaces.Services;
using MicroFinancing.Reporting;
using MicroFinancing.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MicroFinancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class ReportingController : ControllerBase
    {
        private readonly IReportingService _reportingService;

        public ReportingController(IReportingService reportingService)
        {
            _reportingService = reportingService;
        }
        [HttpGet("Download")]
        public async Task<IActionResult> Download( long customerId)
        {
            var report = new rptStatementofAccount()
            {
                DataSource = await _reportingService.StatementOfAccount(customerId)
            };
            using var ms=new MemoryStream();
            await report.ExportToPdfAsync(ms);
            return File(ms.ToArray(),"application/pdf","SOA.pdf");
        }
    }
}
