using MicroFinancing.Core.Common;
using MicroFinancing.Interfaces.Services;
using Syncfusion.Blazor;

namespace MicroFinancing.Services;

public sealed class BatchAdaptor : DataAdaptor
{
    private readonly IUserService _userService;
    private readonly IBatchService _batchService;
    private readonly ICustomerService _customerService;

    public BatchAdaptor(ICustomerService customerService,
        IUserService userService,
        IBatchService batchService)
    {
        _customerService = customerService;
        _userService = userService;
        _batchService = batchService;
    }
    public override async Task<object> ReadAsync(DataManagerRequest dm, string? key = null)
    {

        try
        {
            return await _batchService.BatchGridAdaptor(dm);
        }
        catch (Exception e)
        {
            throw;
        }
    }
}