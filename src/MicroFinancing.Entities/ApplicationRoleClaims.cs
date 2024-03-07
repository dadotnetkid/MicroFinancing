using Microsoft.AspNetCore.Identity;

namespace MicroFinancing.Entities;

public sealed class ApplicationRoleClaims : IdentityRoleClaim<string>
{
    public ApplicationRole? Role { get; set; }
}