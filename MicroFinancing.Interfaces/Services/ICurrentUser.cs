using System.Security.Claims;

namespace MicroFinancing.Interfaces.Services;

public interface ICurrentUser
{
    string UserId { get; }
    string FullName { get; }
    ClaimsPrincipal User { get; }
}