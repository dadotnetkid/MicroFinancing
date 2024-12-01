using System.Text.Json;
using AutoMapper;
using MicroFinancing.Interfaces.Services;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace MicroFinancing.WebAssembly.Services.Adaptors;

public sealed class LendingSummaryAdaptor : DataAdaptor
{
    private readonly ILendingClient _lendingClient;
    private readonly ICurrentUser _currentUser;

    public LendingSummaryAdaptor(ILendingClient lendingClient, ICurrentUser currentUser)
    {
        _lendingClient = lendingClient;
        _currentUser = currentUser;
    }

    public override async Task<object> ReadAsync(DataManagerRequest dm, string? key = null)
    {
        /*object query = default!;

        if (await _userService.IsAuthorizeAsync(ClaimsConstant.Customer.ViewAllCustomer))
        {
            query = await _lendingService.GetSummary(dm, null);
            return query;
        }*/

        var query = await _lendingClient.GetSummaryAsync(JsonSerializer.Serialize(dm));
        /*return new DataResult()
        {
            Aggregates = query.Data.Aggregates,
            Count = query.Data.Count,
            FilteredRecords = query.Data.FilteredRecords,
            Result = query.Data.Result,
        };*/

        return new DataResult()
        {
            Count = query.Count,
            Result = query.Result
        };
    }
}