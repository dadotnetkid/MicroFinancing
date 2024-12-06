using MicroFinancing.Core.Enumeration;

namespace MicroFinancing.DataTransferModel;

public sealed class CreateUpdateUserDTM
{
    public CreateUpdateUserDTM()
    {

    }
    public string? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public List<string> UserRole { get; set; } = [];
    public BranchEnum.Branch Branch { get; set; }
    public bool IsEmployee { get; set; }
    public decimal? BasicPay { get; set; }
}