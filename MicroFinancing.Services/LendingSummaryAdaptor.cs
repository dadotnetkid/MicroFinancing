using MicroFinancing.Core.Common;
using MicroFinancing.Interfaces.Services;
using Syncfusion.Blazor;

namespace MicroFinancing.Services;

public sealed class LendingSummaryAdaptor : DataAdaptor
{
    private readonly ILendingService _lendingService;
    private readonly ICurrentUser _currentUser;
    private readonly IUserService _userService;

    public LendingSummaryAdaptor(ILendingService lendingService, ICurrentUser currentUser, IUserService userService)
    {
        _lendingService = lendingService;
        _currentUser = currentUser;
        _userService = userService;
    }
    public override async Task<object> ReadAsync(DataManagerRequest dm, string? key = null)
    {
        object query = default!;

        if (await _userService.IsAuthorizeAsync(ClaimsConstant.Customer.ViewAllCustomer))
        {
            query = await _lendingService.GetSummary(dm, null);
            return query;
        }

        query = await _lendingService.GetSummary(dm, _currentUser.UserId);
        return query;
    }
}