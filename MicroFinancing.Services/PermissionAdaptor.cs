using MicroFinancing.Core.Common;
using MicroFinancing.Interfaces.Services;
using Syncfusion.Blazor;

namespace MicroFinancing.Services;

public class PermissionAdaptor : DataAdaptor
{
    private readonly IPermissionService _permissionService;

    public PermissionAdaptor(IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }
    public override Task<object> ReadAsync(DataManagerRequest dataManagerRequest, string key = null)
    {
        return _permissionService.GetPermissions().ToDatResult(dataManagerRequest);
    }
}