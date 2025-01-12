namespace MicroFinancing.DataTransferModel;

public sealed class LendingForApprovalGridDTM : BaseLendingDTM
{

    public bool IsRestruct { get; set; }
    public bool IsPaid { get; set; }
    public long ParentId { get; set; }
    public decimal TotalCredit { get; set; }
    public bool IsActive { get; set; }
    public string LendingNumber { get; set; }
    public decimal? PreviousBalance { get; set; }
    public decimal AvailableAmountToRelease { get; set; }
}
