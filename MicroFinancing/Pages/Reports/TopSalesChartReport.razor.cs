using MicroFinancing.Services;

using Microsoft.AspNetCore.Components;

namespace MicroFinancing.Pages.Reports;

public partial class TopSalesChartReport
{
    public DateTime? DateTo { get; set; }
    public DateTime? DateFrom { get; set; }
    public TopSalesChart TopSalesChartRef { get; set; }

    private async Task Search()
    {
        await TopSalesChartRef.Render(DateFrom, DateTo);
    }
}
