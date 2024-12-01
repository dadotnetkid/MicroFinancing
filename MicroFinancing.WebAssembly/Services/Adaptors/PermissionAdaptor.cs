using MicroFinancing.WebAssembly.Common;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;

namespace MicroFinancing.WebAssembly.Services.Adaptors;

public class PermissionAdaptor : DataAdaptor
{
    private readonly IPermissionClient _permissionService;

    public PermissionAdaptor(IPermissionClient permissionService)
    {
        _permissionService = permissionService;
    }

    public override async Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
    {
        var res = await _permissionService.GetPermissionsAsync(dataManagerRequest.ToJson());
        if (!dataManagerRequest.RequiresCounts)
        {
            return res.Result;
        }

        return new DataResult()
        {
            Result = res.Result,
            Count = res.Count
        };
    }
}