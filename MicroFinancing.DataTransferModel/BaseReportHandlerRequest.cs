using Syncfusion.Blazor;

namespace MicroFinancing.DataTransferModel;

public class BaseReportHandlerRequest
{
    public string DataManagerRequest { get; set; }
    public ReportType ReportType { get; set; }
}
public enum ReportType
{
    PaymentSummary
}