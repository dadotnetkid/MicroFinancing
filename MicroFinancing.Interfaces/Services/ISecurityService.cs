using MicroFinancing.DataTransferModel;

namespace MicroFinancing.Interfaces.Services;

public interface ISecurityService
{
    Task<object?> CreateToken(SecurityDTM.LoginModel loginModel);
}