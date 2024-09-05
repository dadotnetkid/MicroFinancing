using MicroFinancing.DataTransferModel;
using System.Data;
using System.Dynamic;

using Syncfusion.Blazor;

namespace MicroFinancing.Interfaces.Services;

public interface IReportingService
{
    Task<List<StatementofAccountDTM>> StatementOfAccount(long customerId);
    public Task<object> GetCollectionSummaryReports(DataManagerRequest dm);
    public Task<List<ExpandoObject>> GetCustomersByCollectorSummaryReport(string collectorId, int month, int year);

}