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
        private readonly IRepository<Payment, long> _paymentRepository;

        public DashboardService(IRepository<Customers, long> customerRepository,
                                IRepository<Payment, long> paymentRepository)
        {
            _customerRepository = customerRepository;
            _paymentRepository = paymentRepository;
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
                                                              .Where(c => !c.IsRestruct))
                                            .SumAsync(x => x.ItemAmount + x.Amount),
                TotalCustomer = await customers.CountAsync()
            };

            return res;
        }

        public async Task<List<decimal?>> GetRenderChart(DateTime dateFrom, DateTime dateTo)
        {

            var payment = await _paymentRepository.Entity
                                                  .Where(c => c.PaymentType != PaymentEnum.PaymentType.System)
                                                  .Where(c => c.PaymentDate >= dateFrom && c.PaymentDate < dateTo)
                                                  .GroupBy(c => c.Creator.Branch)
                                                  .Select(c => new
                                                  {
                                                      Branch = c.Key,
                                                      Collection = c.Sum(s => s.PaymentAmount)
                                                  }).OrderBy(c => c.Branch)
                                                  .ToListAsync();

            return
            [
                payment.FirstOrDefault(c => c.Branch == BranchEnum.Branch.NuevaVizcaya)
                       ?.Collection,

                payment.FirstOrDefault(c => c.Branch == BranchEnum.Branch.Isabela)
                       ?.Collection

            ];
        }

        public async Task<List<ChartCollectorDto>> GetRenderChartByBranchAndDate(BranchEnum.Branch enumBranch,
                                                                                 DateTime dateFrom,
                                                                                 DateTime dateTo)
        {

            var payment = await _paymentRepository.Entity
                                                  .Where(c => c.PaymentType != PaymentEnum.PaymentType.System)
                                                  .Where(c => c.PaymentDate >= dateFrom && c.PaymentDate < dateTo)
                                                  .Where(c => c.Creator.Branch == enumBranch)
                                                  .GroupBy(c => new { c.Creator.FullName })
                                                  .Select(c => new ChartCollectorDto()
                                                  {
                                                      CollectorName = c.Key.FullName,
                                                      Amount = c.Sum(s => s.PaymentAmount)
                                                  }).OrderBy(c => c.CollectorName)
                                                  .ToListAsync();

            return payment;
        }
    }
}
