using MicroFinancing.Core.Common;
using MicroFinancing.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace MicroFinancing.Services
{
    public sealed class PaymentAdaptor : DataAdaptor
    {
        private readonly IPaymentService _paymentService;
        private readonly IServiceProvider _serviceProvider;

        public PaymentAdaptor(IPaymentService paymentService, IServiceProvider serviceProvider)
        {
            _paymentService = paymentService; // serviceProvider.CreateScope().ServiceProvider.GetRequiredService<IPaymentService>();
            _serviceProvider = serviceProvider;
        }
        public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string? key = null)
        {
            var customerId = dataManagerRequest.Params.FirstOrDefault(x => x.Key == "CustomerId").Value?.ToString();

            if (!string.IsNullOrEmpty(customerId))
            {
                var id = Convert.ToInt64(customerId);

                return await _paymentService.Get()
                                            .Where(x => x.CustomerId == id).ToDataResult(dataManagerRequest);
            }
            
            if (dataManagerRequest.Params.Any(x => x.Key == "DateToday") && dataManagerRequest.Params.Any(x => x.Key == "FilterByUserId"))
            {
                var dateStart = Convert.ToDateTime(dataManagerRequest.Params.FirstOrDefault(x => x.Key == "DateToday").Value);
                var dateEnd = dateStart.AddDays(1).AddSeconds(-1);

                var filterByUserId = dataManagerRequest.Params.FirstOrDefault(x => x.Key == "FilterByUserId").Value.ToString();
                return await _paymentService.Get()
                    .Where(x => x.CreatedAt >= dateStart && x.CreatedAt <= dateEnd)
                    .Where(x => x.CreatedByUserId == filterByUserId)
                    .ToDataResult(dataManagerRequest);
            }
            return Task.CompletedTask;
        }
    }
}
