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
            return await _customerService.GetCustomer().ToDataResult(dm);
        }
    }
}
