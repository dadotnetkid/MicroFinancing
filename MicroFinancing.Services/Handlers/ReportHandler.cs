using System.Resources;
using System.Text.Json;

using Syncfusion.Blazor;

namespace MicroFinancing.Services.Handlers;

public class ReportHandler
{
    private readonly IEnumerable<IReportHandler> _reportHandlers;

    public ReportHandler(IEnumerable<IReportHandler> reportHandlers)
    {
        _reportHandlers = reportHandlers;
    }

    public async Task<object> Generate(BaseReportHandlerRequest? payload)
    {
        foreach (var reportHandler in _reportHandlers)
        {
            if (reportHandler.ReportType != payload.ReportType)
            {
                continue;
            }

            var result = await reportHandler.Generate(payload);

            return result;
        }

        throw new ArgumentNullException("No Report Found");
    }

}