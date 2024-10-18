using System.ComponentModel.DataAnnotations;

using MicroFinancing.Core.Enumeration;

namespace MicroFinancing.DataTransferModel;

public sealed class UserGridDTM
{
    public string? Id { get; set; }

    [Display(Name = "First Name")] public string? FirstName { get; set; }

    [Display(Name = "Last Name")] public string? LastName { get; set; }

    [Display(Name = "Email")] public string? Email { get; set; }

    [Display(Name = "Username")] public string? UserName { get; set; }

    [Display(Name = "User Roles")] public IEnumerable<ApplicationRoleDTM>? UserRoles { get; set; }

    public string FullName { get; set; }
    public BranchEnum.Branch Branch { get; set; }
}