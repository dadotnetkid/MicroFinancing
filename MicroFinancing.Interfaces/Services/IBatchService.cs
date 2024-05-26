using Syncfusion.Blazor;

namespace MicroFinancing.Interfaces.Services;

public interface IBatchService
{
    Task<object> BatchGridAdaptor(DataManagerRequest dm);
}