using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using Syncfusion.Blazor;

namespace MicroFinancing.Interfaces.Services;

public interface IUserService
{
    Task<CreateUpdateUserDTM> CreateUser(CreateUpdateUserDTM item);
    Task UpdateUser(CreateUpdateUserDTM user);
    Task DeleteUser(string? userId);
    Task ResetPassword(ResetPasswordUserDTM resetPasswordUserDtm);
    Task<string> GetUserId();
    Task<bool> IsAuthorizeAsync(string policy, bool showToast=true);
    bool IsAuthorize(string policy, bool showToast = true);
    Task AddRoles(CreateUpdateUserDTM user);
    Task<bool> IsInRoleAsync(params string[] roles);
    Task<object> GetUsers(DataManagerRequest? dataManager);
}