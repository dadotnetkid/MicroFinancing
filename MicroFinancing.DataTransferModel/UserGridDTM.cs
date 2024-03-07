using MicroFinancing.Entities;
using System.ComponentModel.DataAnnotations;

namespace MicroFinancing.DataTransferModel
{
    public sealed class UserGridDTM
    {
        public UserGridDTM()
        {

        }
        public string? Id { get; set; }
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [Display(Name = "Email")]
        public string? Email { get; set; }
        [Display(Name = "Username")]
        public string? UserName { get; set; }
        [Display(Name = "User Roles")]
        public IEnumerable<ApplicationRoleDTM>? UserRoles { get; set; }

        public string FullName { get; set; }
    }

    public sealed class ApplicationRoleDTM
    {
        public  string? Id { get; set; }
        public  string? Name { get; set; }
        public  string? NormalizedName { get; set; }
        public  string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    }

    public sealed class ResetPasswordUserDTM
    {
        public string? UserId { get; set; }
        public string Password { get; set; }
    }
}