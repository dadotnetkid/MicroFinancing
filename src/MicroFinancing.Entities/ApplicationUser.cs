using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Identity;

namespace MicroFinancing.Entities
{
    public class ApplicationUser : IdentityUser
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
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public ICollection<ApplicationUserToken> UserTokens { get; set; }
        public ICollection<ApplicationUserLogin> UserLogins { get; set; }
        public ICollection<ApplicationUserClaims> UserClaims { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public string FullName { get; set; }
    }
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole()
        {
            UserRoles = new HashSet<ApplicationUserRole>();
            RoleClaims = new HashSet<ApplicationRoleClaims>();
        }
        public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
        public virtual ICollection<ApplicationRoleClaims> RoleClaims { get; set; }
    }
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationRole? Role { get; set; }
        public virtual ApplicationUser? User { get; set; }

    }
    public class ApplicationUserToken : IdentityUserToken<string>
    {
        public virtual ApplicationUser? User { get; set; }
    }
    public class ApplicationUserLogin : IdentityUserLogin<string>
    {
        public virtual ApplicationUser? User { get; set; }
    }
    public class ApplicationUserClaims : IdentityUserClaim<string>
    {
        public virtual ApplicationUser? User { get; set; }
    }
    public class ApplicationRoleClaims : IdentityRoleClaim<string>
    {
        public virtual ApplicationRole? Role { get; set; }
    }


}
