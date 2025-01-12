using MicroFinancing.Interfaces.Services;

using Syncfusion.Blazor;

namespace MicroFinancing.Services;

public sealed class CustomerAdaptor : DataAdaptor
{
    private readonly ICustomerService _customerService;
    private readonly IUserService _userService;

    public CustomerAdaptor(ICustomerService customerService,
                           IUserService userService)
    {
        _customerService = customerService;
        _userService = userService;
    }

    public override async Task<object> ReadAsync(DataManagerRequest dm,
                                                 string? key = null)
    {
        try
        {
            if (await _userService.IsAuthorizeAsync(ClaimsConstant.Customer.ViewAllCustomer, false))
            {
                var a = await _customerService.GetCustomer()
                                              .ToDataResult(dm);

                return a;
            }

            var res = await _customerService.GetCustomerByCollector(await _userService.GetUserId())
                                            .ToDataResult(dm);

            return res;
        }
        catch (Exception e)
        {
            return new DataManager();
        }
    }
}
