using MicroFinancing.DataTransferModel;
using MicroFinancing.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MicroFinancing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        [HttpGet(nameof(TopSalesChartToday))]
        public async Task<ActionResult<List<decimal?>>> TopSalesChartToday(DateTime? dateFrom, DateTime? dateTo)
        {
            var renderChart = await _dashboardService.GetRenderChart(dateFrom.GetValueOrDefault(), dateTo.GetValueOrDefault());

            return Ok(BaseResultDto<List<decimal?>>.Success(renderChart));
        }
    }
}
