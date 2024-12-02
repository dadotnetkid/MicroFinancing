using MicroFinancing.Infrastructure.Enums;

namespace MicroFinancing.DataTransferModel;

public sealed class TermDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long Number { get; set; }
    public TermEnum TermEnum { get; set; }
}

public sealed class PaymentForApprovalDto
{
    public string CollectorName { get; set; }

    public List<PaymentsForApprovalByDateDto> PaymentByDate { get; set; } = new();
    public decimal? TotalAmount { get; set; }
}

public sealed class PaymentsForApprovalByDateDto
{
    public string PaymentDate { get; set; }
    public List<PaymentsForApprovalDto> Payments { get; set; } = new();
    public decimal? TotalAmount { get; set; }
}

public sealed class PaymentsForApprovalDto
{
    public decimal? PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }
    public long PaymentId { get; set; }
    public string CustomerName { get; set; }
    public string LendingNumber { get; set; }
}