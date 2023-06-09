using MicroFinancing.DataTransferModel;

namespace MicroFinancing.Services;

public interface IDashboardService
{
    Task<DashboardDTM> GetDashboard();
}