using MicroFinancing.Core.Common;
using MicroFinancing.Interfaces.Services;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinancing.Services
{
    public sealed class CustomerAdaptor : DataAdaptor
    {
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;

        public CustomerAdaptor(ICustomerService customerService,IUserService userService)
        {
            _customerService = customerService;
            _userService = userService;
        }
        public override async Task<object> ReadAsync(DataManagerRequest dm, string? key = null)
        {
            if (await _userService.IsInRoleAsync("Administrator"))
            {
                return await _customerService.GetCustomer().ToDataResult(dm);
            }

            return await _customerService.GetCustomerByCollector(await _userService.GetUserId()).ToDataResult(dm);
        }
    }
}
