using MicroFinancing.Interfaces.Services;
using MicroFinancing.WebAssembly.Services;

namespace MicroFinancing.WebAssembly.Pages.Dashboard;

public partial class TopSalesChartReport
{
    public DateTime? DateTo { get; set; }
    public DateTime? DateFrom { get; set; }
    public TopSalesChart TopSalesChartRef { get; set; }
    [Inject] IDashboardClient DashboardClient { get; set; }
    [Inject] ICurrentUser CurrentUser{ get; set; }
    private async Task Search()
    {
       
        await TopSalesChartRef.Render(DateFrom, DateTo);
    }
}
