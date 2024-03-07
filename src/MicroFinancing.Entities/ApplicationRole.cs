using Microsoft.AspNetCore.Identity;

namespace MicroFinancing.Entities;

public sealed class ApplicationRole : IdentityRole<string>
{
    public ApplicationRole()
    {
        UserRoles = new HashSet<ApplicationUserRole>();
        RoleClaims = new HashSet<ApplicationRoleClaims>();
    }

    public ICollection<ApplicationUserRole> UserRoles { get; set; }
    public ICollection<ApplicationRoleClaims> RoleClaims { get; set; }
}