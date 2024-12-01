using MicroFinancing.DataTransferModel;
using Syncfusion.Blazor;

namespace MicroFinancing.Interfaces.Services;

public interface IPermissionService
{
    Task<object> GetPermissions(DataManagerRequest dm);
    Task Update(CreateUpdatePermissionDTM createUpdatePermissionDtm);
    Task Create(CreateUpdatePermissionDTM permission);
}