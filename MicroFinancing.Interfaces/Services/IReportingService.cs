using MicroFinancing.DataTransferModel;
using System.Data;
using System.Dynamic;

namespace MicroFinancing.Interfaces.Services;

public interface IReportingService
{
    Task<List<StatementofAccountDTM>> StatementOfAccount(long customerId);
    public IQueryable<CollectionSummaryReportDTM> GetCollectionSummaryReports();
    public Task<List<ExpandoObject>> GetCustomersByCollectorSummaryReport(string collectorId, int month, int year);

}