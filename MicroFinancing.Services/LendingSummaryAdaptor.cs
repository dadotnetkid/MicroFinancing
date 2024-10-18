using MicroFinancing.Core.Common;
using MicroFinancing.Interfaces.Services;
using Syncfusion.Blazor;

namespace MicroFinancing.Services;

public sealed class LendingSummaryAdaptor : DataAdaptor
{
    private readonly ILendingService _lendingService;
    private readonly ICurrentUser _currentUser;

    public LendingSummaryAdaptor(ILendingService lendingService, ICurrentUser currentUser)
    {
        _lendingService = lendingService;
        _currentUser = currentUser;
    }
    public override async Task<object> ReadAsync(DataManagerRequest dm, string? key = null)
    {
        var isAdmin = _currentUser.User.IsInRole("Administrator");

        object query = default!;

        if (isAdmin)
        {
            query = await _lendingService.GetSummary(dm, null);
            return query;
        }

        query = await _lendingService.GetSummary(dm, _currentUser.UserId);
        return query;
    }
}