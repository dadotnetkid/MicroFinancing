using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;

namespace MicroFinancing.Interfaces.Services;

public interface ISecurityService
{
    Task<object?> CreateToken(SecurityDto.LoginModel loginModel);
    Task<RefreshToken> CreateRefreshToken(string userName);

    Task<RefreshToken?> RefreshToken(string refreshToken);
}