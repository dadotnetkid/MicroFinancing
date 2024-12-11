using System.Text.Json;

using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace MicroFinancing.MobileApp.Services;

public class CustomerClientAdaptor : DataAdaptor
{
    private readonly ICustomersClient _client;

    public CustomerClientAdaptor(ICustomersClient client)
    {
        _client = client;
    }
    public override async Task<object> ReadAsync(DataManagerRequest dm, string key = null)
    {
        var json = JsonSerializer.Serialize(dm);
        var response = await _client.GetCustomersAdaptorAsync(json);

        return new DataResult()
        {
            Result = response.Result,
            Count = response.Count ?? 0
        };
    }
}
