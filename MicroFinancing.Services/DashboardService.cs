using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinancing.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IRepository<Customers> _customerRepository;

        public DashboardService(IRepository<Customers> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<DashboardDTM> GetDashboard()
        {
            var customers = _customerRepository.Entity;
            var res = new DashboardDTM()
            {
                TotalAmountPaid =await customers.SelectMany(s=>s.Payments).SumAsync(x=>x.PaymentAmount),
                TotalLoans =await customers.SelectMany(s => s.Lending).SumAsync(x=>x.ItemAmount+x.Amount),
                TotalCustomer =await customers.CountAsync()
            };

            return res;
        }
    }
}
