using Syncfusion.Blazor;

namespace MicroFinancing.Services;

public interface ITermService
{
    Task<object> TermGrid(DataManagerRequest dm);
}