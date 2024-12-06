using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinancing.Interfaces.Services;

namespace MicroFinancing.Services
{
    public sealed class LendingAdaptor : DataAdaptor
    {
        private readonly ILendingService _lendingService;

        public LendingAdaptor(ILendingService lendingService)
        {
            _lendingService = lendingService;
        }
        public override async Task<object> ReadAsync(DataManagerRequest dm, string? key = null)
        {
            var query = _lendingService.Get()
                                       .OrderByDescending(c => c.Id)
                                       .AsQueryable();
            if (dm.Params != null && dm.Params.Any(x => x.Key == "CustomerId"))
            {
                var customerId = dm.Params.FirstOrDefault(x => x.Key == "CustomerId").Value.ToTypeOf<long>();
                query = query.Where(x => x.CustomerId == customerId);
            }
            return await query.ToDataResult(dm);
        }
    }
}
