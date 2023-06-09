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

        public CustomerAdaptor(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        public override async Task<object> ReadAsync(DataManagerRequest dm, string? key = null)
        {
            return await _customerService.GetCustomer().ToDatResult(dm);
        }
    }
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
                return await _customerService.GetCustomerByCollector(await _userService.GetUserId()).ToDatResult(dm);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
