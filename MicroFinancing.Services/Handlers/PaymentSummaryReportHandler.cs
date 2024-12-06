using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

using Syncfusion.Blazor;

namespace MicroFinancing.Services.Handlers;

public class PaymentSummaryReportHandler : IReportHandler
{
    private readonly IRepository<Payment, long> _paymentRepository;

    public PaymentSummaryReportHandler(IRepository<Payment, long> paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public ReportType ReportType { get; set; } = ReportType.PaymentSummary;

    public async Task<object?> Generate(BaseReportHandlerRequest? payload)
    {

        if (payload is null)
        {
            return null;
        }

        if (payload.ReportType != ReportType.PaymentSummary)
        {
            return null;
        }

        var dm = JsonSerializer.Deserialize<DataManagerRequest>(payload.DataManagerRequest);

        if (dm is null)
        {
            return new PaymentSummaryDto();
        }

        var dateFrom = dm.GetByKey<DateTime>("DateFrom");

        var dateTo = dm.GetByKey<DateTime>("DateTo");

        var creatorUserId = dm.GetByKey<string>("CreatorUserId");

        dateTo = dateTo.AddDays(1);

        return (await _paymentRepository
                      .Entity
                      .Where(c => c.CreatorUserId == creatorUserId)
                      .Where(c=>!c.Lending.IsDeleted)
                      .Where(c => c.PaymentDate >= dateFrom && c.PaymentDate < dateTo)
                      .GroupBy(c => c.Creator.FullName)
                      .Select(c => new PaymentSummaryDto()
                      {
                          Amount = c.Sum(s => s.PaymentAmount),
                          CollectorName = c.Key,
                          PaymentSummaryListByDate = c.GroupBy(b => ScalarFunctionHelper.Format(b.PaymentDate, "MM-dd-yyyy"))
                                                      .Select(b => new PaymentSummaryListByDateDto()
                                                      {
                                                          Amount = b.Sum(s => s.PaymentAmount),
                                                          PaymentDate = b.Key,
                                                          PaymentSummaryList = b.Select(p => new PaymentSummaryListDto()
                                                          {
                                                              LendingNumber = p.Lending.LendingNumber,
                                                              CustomerName = p.Customers.FullName,
                                                              PaymentAmount = p.PaymentAmount,
                                                              PaymentDate = p.PaymentDate
                                                          }).ToList()
                                                      }).ToList()
                      })
                      .ToDataResult(dm))
            .ToDataResultDto<PaymentSummaryDto>();
    }
}
