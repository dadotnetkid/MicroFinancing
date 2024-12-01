using MicroFinancing.Core.Enumeration;
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
        [HttpGet(nameof(GetRenderChart))]
        public async Task<ActionResult<BaseResultDto<List<decimal?>>>> GetRenderChart(DateTime? dateFrom, DateTime? dateTo)
        {
            var renderChart = await _dashboardService.GetRenderChart(dateFrom.GetValueOrDefault(), dateTo.GetValueOrDefault());

            return Ok(BaseResultDto<List<decimal?>>.Success(renderChart));
        }
        [HttpGet(nameof(GetRenderChartByBranchAndDate))]
        public async Task<ActionResult<BaseResultDto<List<ChartCollectorDto>>>> GetRenderChartByBranchAndDate(BranchEnum.Branch branch, DateTime? dateFrom, DateTime? dateTo)
        {
            var renderChart = await _dashboardService.GetRenderChartByBranchAndDate(branch, dateFrom.GetValueOrDefault(), dateTo.GetValueOrDefault());

            return Ok(BaseResultDto<List<ChartCollectorDto>>.Success(renderChart));
        }
        [HttpGet(nameof(GetDashboard))]
        public async Task<ActionResult<BaseResultDto<DashboardDTM>>> GetDashboard()
        {
            var dashboard = await _dashboardService.GetDashboard();

            return Ok(BaseResultDto<DashboardDTM>.Success(dashboard));
        }
    }
}
