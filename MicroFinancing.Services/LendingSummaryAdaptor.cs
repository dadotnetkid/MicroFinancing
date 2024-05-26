using MicroFinancing.Core.Common;
using MicroFinancing.Interfaces.Services;
using Syncfusion.Blazor;

namespace MicroFinancing.Services;

public sealed class LendingSummaryAdaptor : DataAdaptor
{
    private readonly ILendingService _lendingService;

    public LendingSummaryAdaptor(ILendingService lendingService)
    {
        _lendingService = lendingService;
    }
    public override async Task<object> ReadAsync(DataManagerRequest dm, string? key = null)
    {
        var query = _lendingService.GetSummary();
        return await query.ToDataResult(dm);
    }
}