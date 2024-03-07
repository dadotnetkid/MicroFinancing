using Microsoft.AspNetCore.Identity;

namespace MicroFinancing.Entities;

public sealed class ApplicationUserLogin : IdentityUserLogin<string>
{
    public ApplicationUser? User { get; set; }
}