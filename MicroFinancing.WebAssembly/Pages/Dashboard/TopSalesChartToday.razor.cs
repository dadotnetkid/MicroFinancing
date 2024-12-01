namespace MicroFinancing.WebAssembly.Pages.Dashboard;

public partial class TopSalesChartToday
{
    private TopSalesChart topSalesChartRef;

    private async Task Refresh()
    {
        await topSalesChartRef.Render(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
    }
}
