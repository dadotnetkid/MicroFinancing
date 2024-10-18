using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MicroFinancing.Core.Enumeration;

namespace MicroFinancing.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IRepository<Customers, long> _customerRepository;

        public DashboardService(IRepository<Customers, long> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<DashboardDTM> GetDashboard()
        {
            var customers = _customerRepository.Entity;
            var res = new DashboardDTM()
            {
                TotalAmountPaid = await customers
                                       .SelectMany(s => s.Payments
                                                         .Where(c => c.PaymentType != PaymentEnum.PaymentType.System))
                                       .SumAsync(x => x.PaymentAmount),

                TotalLoans = await customers.SelectMany(s => s.Lending
                                                              .Where(c=>!c.IsRestruct))
                                            .SumAsync(x => x.ItemAmount + x.Amount),
                TotalCustomer = await customers.CountAsync()
            };

            return res;
        }
    }
}
