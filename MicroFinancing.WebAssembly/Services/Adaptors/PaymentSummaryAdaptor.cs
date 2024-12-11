

using MicroFinancing.WebAssembly.Common;

using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

using DataManagerRequest = Syncfusion.Blazor.DataManagerRequest;

namespace MicroFinancing.WebAssembly.Services.Adaptors;

public class PaymentSummaryAdaptor : DataAdaptor
{
    private readonly IReportingClient _reportingClient;

    public PaymentSummaryAdaptor(IReportingClient reportingClient)
    {
        _reportingClient = reportingClient;
    }

    public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest,
                                                 string additionalParam = null)
    {
        var res = await _reportingClient.GeneratePaymentSummaryAsync(new BaseReportHandlerRequest()
        {
            DataManagerRequest = dataManagerRequest.ToJson(),
            ReportType = ReportType.PaymentSummary

        });

        return new DataResult()
        {
            Result = res.Result,
            Count = res.Count ?? 0
        };
    }
}
