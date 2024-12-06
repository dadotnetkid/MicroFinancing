namespace MicroFinancing.Services.Handlers;

public interface IReportHandler
{
    public ReportType ReportType { get; set; }
    public Task<object?> Generate(BaseReportHandlerRequest? payload);
}
