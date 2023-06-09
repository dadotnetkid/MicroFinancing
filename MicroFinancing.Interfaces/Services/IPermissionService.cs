using MicroFinancing.DataTransferModel;

namespace MicroFinancing.Interfaces.Services;

public interface IPermissionService
{
    IQueryable<PermissionGridDTM> GetPermissions();
    Task Update(CreateUpdatePermissionDTM createUpdatePermissionDtm);
    Task Create(CreateUpdatePermissionDTM permission);
}