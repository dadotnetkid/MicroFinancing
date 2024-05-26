using Syncfusion.Blazor;

namespace MicroFinancing.Services;

public class TermAdaptor : DataAdaptor
{
    private readonly ITermService _termService;

    public TermAdaptor(ITermService termService)
    {
        _termService = termService;
    }
    public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string additionalParam = null)
    {
        return await _termService.TermGrid(dataManagerRequest);
    }
}