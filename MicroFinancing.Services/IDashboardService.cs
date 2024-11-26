using MicroFinancing.Core.Enumeration;
using MicroFinancing.DataTransferModel;

namespace MicroFinancing.Services;

public interface IDashboardService
{
    Task<DashboardDTM> GetDashboard();

    Task<List<decimal?>> GetRenderChart(DateTime dateFrom,
                                        DateTime dateTo);

    Task<List<ChartCollectorDto>> GetRenderChartByBranchAndDate(BranchEnum.Branch enumBranch,
                                                                DateTime dateFrom,
                                                                DateTime dateTo);
}