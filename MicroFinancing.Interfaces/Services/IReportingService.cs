using MicroFinancing.DataTransferModel;
using System.Data;
using System.Dynamic;

using Syncfusion.Blazor;

namespace MicroFinancing.Interfaces.Services;

public interface IReportingService
{
    Task<List<StatementofAccountDTM>> StatementOfAccount(long customerId);
    Task<List<StatementofAccountDTM>> StatementOfAccountByLendingId(long lendingId);
    public Task<object> GetCollectionSummaryReports(DataManagerRequest dm);
    public Task<List<ExpandoObject>> GetCustomersByCollectorSummaryReport(string collectorId,
        DateTime startDate, DateTime endDate);

}