using Microsoft.AspNetCore.Identity;

namespace MicroFinancing.Entities;

public sealed class ApplicationUserRole : IdentityUserRole<string>
{
    public ApplicationRole? Role { get; set; }
    public ApplicationUser? User { get; set; }
}