using System.Text.Json;
using System.Text.Json.Serialization;
using Syncfusion.Blazor;

namespace MicroFinancing.WebAssembly.Common;

public static class DataManagerHelper
{
    public static string ToJson(this DataManagerRequest dm)
    {
        return JsonSerializer.Serialize(dm);
    }

}
