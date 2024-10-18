using MicroFinancing.Core.Enumeration;

using Microsoft.AspNetCore.Identity;

namespace MicroFinancing.Entities;

public sealed class ApplicationUser : IdentityUser, ISoftDeletable
{
    public ApplicationUser()
    {
        UserRoles = new HashSet<ApplicationUserRole>();
        UserTokens = new HashSet<ApplicationUserToken>();
        UserLogins = new HashSet<ApplicationUserLogin>();
        UserClaims = new HashSet<ApplicationUserClaims>();
        Payments = new HashSet<Payment>();
    }

    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public ICollection<ApplicationUserRole> UserRoles { get; set; }
    public ICollection<ApplicationUserToken> UserTokens { get; set; }
    public ICollection<ApplicationUserLogin> UserLogins { get; set; }
    public ICollection<ApplicationUserClaims> UserClaims { get; set; }
    public ICollection<Payment> Payments { get; set; }
    public string FullName { get; set; }
    public DateTimeOffset? DeletionAt { get; set; }
    public bool IsDeleted { get; set; }
    public BranchEnum.Branch Branch { get; set; }
}