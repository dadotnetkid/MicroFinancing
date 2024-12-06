using System.Data;
using System.Dynamic;

using MicroFinancing.Interfaces.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;

namespace MicroFinancing.Services;

public sealed class ReportingService : IReportingService
{
    private readonly IRepository<Customers, long> _customerRepository;
    private readonly IRepository<Lending, long> _lendingRepository;
    private readonly IRepository<Payment, long> _paymentRepository;
    private readonly UserManager<ApplicationUser> _userManager;

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

        var list = await entity.Select(x => new StatementofAccountDTM
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

            list.PaymentDates.Add(new PaymentDateDTM
            {
                PaymentDate = dateFrom,
                AmountPaid = payment ?? 0,
                Notes = currentDate.Month == list.DueDate.Month && currentDate.Day == 3 ? "Adv. Payment" :
                    dateFrom.DayOfWeek == DayOfWeek.Sunday ? "Not Applicable" : string.Empty
            });

            currentDate = currentDate.AddDays(1);
        }

        return new List<StatementofAccountDTM> { list };
    }

    public async Task<List<StatementofAccountDTM>> StatementOfAccountByLendingId(long lendingId)
    {
        var entity = _lendingRepository
            .Entity
            .Where(x => x.Id == lendingId);

        var list = await entity.Select(x => new StatementofAccountDTM
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

        var payments = _paymentRepository.Entity
            .Where(c => c.IsApproved)
            .Where(x => x.LendingId == lendingId)
            .Where(c => !c.Override);

        var currentDate = list.ReleaseDate.AddDays(1);

        while (currentDate <= list.DueDate)
        {
            var dateFrom = Convert.ToDateTime(currentDate.ToShortDateString());
            var dateTo = dateFrom.AddDays(1).AddHours(-1);
            var payment = payments.Where(x => x.PaymentDate >= dateFrom && x.PaymentDate <= dateTo)
                .Sum(x => x.PaymentAmount);

            list.PaymentDates.Add(new PaymentDateDTM
            {
                PaymentDate = dateFrom,
                AmountPaid = payment ?? 0,
                Notes = currentDate.Month == list.DueDate.Month && currentDate.Day == 3 ? "Adv. Payment" :
                    dateFrom.DayOfWeek == DayOfWeek.Sunday ? "Not Applicable" : string.Empty
            });

            currentDate = currentDate.AddDays(1);
        }

        return new List<StatementofAccountDTM> { list };
    }

    public async Task<object> GetCollectionSummaryReports(DataManagerRequest dm)
    {
        var dateFrom = dm.GetByKey<DateTime?>("DateFrom");
        var dateTo = dm.GetByKey<DateTime?>("DateTo");
        var collector = dm.GetByKey<string>("Collector");

        var query = _paymentRepository
            .Entity
            .AsQueryable();

        if (string.IsNullOrEmpty(collector) || dateFrom is null || dateTo is null)
            return Enumerable.Empty<Payment>().AsQueryable().ToDataResult(dm);

        if (dateFrom != null && dateTo != null)
        {
            dateTo = dateTo?.AddDays(1);

            query = query
                .Where(x => x.PaymentDate >= dateFrom && x.PaymentDate < dateTo);
        }

        if (!string.IsNullOrEmpty(collector)) query = query.Where(x => x.CreatorUserId == collector);


        var select = query.Select(x => new CollectionSummaryReportDTM
        {
            EncodedBy = x.Creator.FullName,
            CustomerName = x.Customers.FirstName + " " + x.Customers.LastName,
            PaymentAmount = x.PaymentAmount,
            PaymentDate = x.PaymentDate
        });

        var res = await select.ToDataResult(dm);

        return res;
    }

    public async Task<List<ExpandoObject>> GetCustomersByCollectorSummaryReport(string collectorId,
        DateTime startDate, DateTime endDate)
    {
        var res = _customerRepository.Entity
            .Where(c => c.Lending.Any(l => l.Collector == collectorId && l.IsActive == true))
            .Select(c => new
            {
                CustomerName = c.FullName,
                c.Id,
                c.Payments,
                Lending = c.Lending.FirstOrDefault(c => c.IsActive)
            });

        var currentDate = startDate;
        var expandoList = new List<ExpandoObject>();

        foreach (var customer in res)
        {
            var expandoDict = new ExpandoObject() as IDictionary<string, object?>;
            expandoDict.Add("Id", customer.Id);
            expandoDict.Add("CustomerName", customer.CustomerName);
            expandoDict.Add("Loan Amount", customer.Lending.TotalCredit);
            expandoDict.Add("DueDate", customer.Lending.DueDate);
            currentDate = startDate;

            while (currentDate <= endDate)
            {
                var curDate = currentDate.ToShortDateString();

                var resValue = customer.Payments
                    .FirstOrDefault(x => x.PaymentDate.ToShortDateString() == curDate)
                    ?.PaymentAmount?.ToString("n2");

                if (currentDate < customer.Lending.LendingDate && string.IsNullOrEmpty(resValue))
                    resValue = "Not Started";
                else if (string.IsNullOrEmpty(resValue)) resValue = "Not Paid";

                expandoDict.Add(curDate, resValue);
                currentDate = currentDate.AddDays(1);
            }

            expandoList.Add((ExpandoObject)expandoDict);
        }

        return expandoList;
    }

    public DataTable GetCustomersByCollectorSummaryReport(string collectorId, string monthName)
    {
        throw new NotImplementedException();
    }
}