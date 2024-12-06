using MicroFinancing.Interfaces.Services;
using Syncfusion.Blazor;

namespace MicroFinancing.Services;

public sealed class CustomerComboBoxAdaptor : DataAdaptor
{
    private readonly IUserService _userService;
    private readonly ICustomerService _customerService;

    public CustomerComboBoxAdaptor(ICustomerService customerService, IUserService userService)
    {
        _customerService = customerService;
        _userService = userService;
    }
    public override async Task<object> ReadAsync(DataManagerRequest dm, string? key = null)
    {

        try
        {
            var isInRole = await _userService.IsInRoleAsync("Administrator");

            if(isInRole)
            {
                return await _customerService.GetCustomer().ToDataResult(dm);
            }

            return await _customerService.GetCustomerByCollector(await _userService.GetUserId()).ToDataResult(dm);
        }
        catch (Exception e)
        {
            throw;
        }
    }
}