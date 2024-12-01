using MicroFinancing.Core.Common;
using MicroFinancing.Interfaces.Services;
using Syncfusion.Blazor;

namespace MicroFinancing.Services;

public sealed class CollectionSummaryReportAdaptor:DataAdaptor
{
    private readonly IReportingService _reportingService;

    public CollectionSummaryReportAdaptor(IReportingService reportingService)
    {
        _reportingService = reportingService;
    }
    public override Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
    {

        return _reportingService.GetCollectionSummaryReports(dataManagerRequest);
    }
}