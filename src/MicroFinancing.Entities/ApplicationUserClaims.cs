using Microsoft.AspNetCore.Identity;

namespace MicroFinancing.Entities;

public class ApplicationUserClaims : IdentityUserClaim<string>
{
    public virtual ApplicationUser? User { get; set; }
}