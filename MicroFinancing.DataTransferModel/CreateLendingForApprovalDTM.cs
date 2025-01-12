namespace MicroFinancing.DataTransferModel;

public sealed class CreateLendingForApprovalDTM : BaseLendingDTM
{
    public decimal? PreviousBalance { get; set; }
}
