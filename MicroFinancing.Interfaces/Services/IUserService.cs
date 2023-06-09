using MicroFinancing.DataTransferModel;

namespace MicroFinancing.Interfaces.Services;

public interface IUserService
{
    Task CreateUser(CreateUpdateUserDTM item);
    Task UpdateUser(CreateUpdateUserDTM user);
    Task DeleteUser(string? userId);
    Task ResetPassword(ResetPasswordUserDTM resetPasswordUserDtm);
    Task<string> GetUserId();
    Task<bool> IsAuthorize(string policy, bool showToast=true);
    Task AddRoles(CreateUpdateUserDTM user);
}