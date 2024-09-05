using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinancing.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using System.Dynamic;

using MicroFinancing.Core.Common;

using Syncfusion.Blazor;

namespace MicroFinancing.Services
{
    public sealed class ReportingService : IReportingService
    {
        private readonly IRepository<Customers, long> _customerRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Lending, long> _lendingRepository;
        private readonly IRepository<Payment, long> _paymentRepository;

        public ReportingService(IRepository<Customers, long> customerRepository,
            UserManager<ApplicationUser> userManager,
            IRepository<Lending, long> lendingRepository, IRepository<Payment, long> paymentRepository)
        {
            _customerRepository = customerRepository;
            _userManager = userManager;
            _lendingRepository = lendingRepository;
            _paymentRepository = paymentRepository;
        }

        public async Task<List<StatementofAccountDTM>> StatementOfAccount(long customerId)
        {
            var entity = _lendingRepository.Entity.Where(x => x.CustomerId == customerId).OrderByDescending(x => x.Id);

            var list = await entity.Select(x => new StatementofAccountDTM()
            {
                Collector = x.CollectorUser.FirstName + " " + x.CollectorUser.LastName,
                DueDate = x.DueDate,
                ItemsAmount = x.ItemAmount,
                MoneyAmount = x.Amount,
                ReleaseDate = x.LendingDate,
                CustomerName = x.Customers.FirstName + " " + x.Customers.LastName,
                TotalCredit = x.TotalCredit,
                DailyPayment = x.TotalCredit / x.PaymentDays,
                Interest = x.Interest
            }).FirstOrDefaultAsync();

            var payments = _paymentRepository.Entity.Where(x => x.CustomerId == customerId);
            var currentDate = list.ReleaseDate.AddDays(1);

            while (currentDate <= list.DueDate)
            {


                var dateFrom = Convert.ToDateTime(currentDate.ToShortDateString());
                var dateTo = dateFrom.AddDays(1).AddHours(-1);
                var payment = payments.Where(x => x.PaymentDate >= dateFrom && x.PaymentDate <= dateTo)
                    .Sum(x => x.PaymentAmount);

                list.PaymentDates.Add(new PaymentDateDTM()
                {
                    PaymentDate = dateFrom,
                    AmountPaid = payment ?? 0,
                    Notes = currentDate.Month == list.DueDate.Month && currentDate.Day == 3 ?
                                            "Adv. Payment" :
                                                dateFrom.DayOfWeek == DayOfWeek.Sunday ?
                                                "Not Applicable" : string.Empty
                });

                currentDate = currentDate.AddDays(1);
            }

            return new List<StatementofAccountDTM>() { list };
        }

        public async Task<object> GetCollectionSummaryReports(DataManagerRequest dm)
        {
            var dateFrom = dm.GetByKey<DateTime?>("DateFrom");
            var dateTo = dm.GetByKey<DateTime?>("DateTo");
            var collector = dm.GetByKey<string>("Collector");

            var query = _paymentRepository
                        .Entity
                        .AsQueryable();

            if (dateFrom != null && dateTo != null)
            {
                query = query.Where(x => x.PaymentDate >= dateFrom && x.PaymentDate <= dateTo);
            }

            if (!string.IsNullOrEmpty(collector))
            {
                query = query.Where(x => x.Lending.Collector == collector);
            }


            var select = query.Select(x => new DataTransferModel.CollectionSummaryReportDTM()
            {
                EncodedBy = x.Creator.FullName,
                CustomerName = x.Customers.FirstName + " " + x.Customers.LastName,
                PaymentAmount = x.PaymentAmount,
                PaymentDate = x.PaymentDate,
            });

            var res = await select.ToDataResult(dm);

            return res;
        }

        public DataTable GetCustomersByCollectorSummaryReport(string collectorId, string monthName)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ExpandoObject>> GetCustomersByCollectorSummaryReport(string collectorId, int month, int year)
        {
            var startDate = Convert.ToDateTime($"{month}/01/{year}");
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var res = _lendingRepository.Entity
                .Where(x => x.Collector == collectorId)
                .Where(x => x.IsActive)
                .Select(x => new { x.Id, x.LendingDate, x.DueDate, CustomerName = x.Customers.FullName, x.Payments });

            var currentDate = startDate;
            List<ExpandoObject> expandoList = new List<ExpandoObject>();

            foreach (var customer in res)
            {
                var expandoDict = new ExpandoObject() as IDictionary<string, object?>;
                expandoDict.Add("Id", customer.Id);
                expandoDict.Add("CustomerName", customer.CustomerName);

                while (currentDate <= endDate)
                {
                    var curDate = currentDate.ToShortDateString();
                    var resValue = customer.Payments
                        .FirstOrDefault(x => x.PaymentDate.ToShortDateString() == curDate)
                        ?.PaymentAmount?.ToString("n2");
                    if (currentDate < customer.LendingDate && string.IsNullOrEmpty(resValue))
                    {
                        resValue = "Not Started";
                    }
                    expandoDict.Add(curDate, resValue);
                    currentDate = currentDate.AddDays(1);
                }
                expandoList.Add((ExpandoObject)expandoDict);
            }
            return expandoList;
        }
    }
}
