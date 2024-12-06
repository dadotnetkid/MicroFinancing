namespace MicroFinancing.DataTransferModel;

public class PaymentSummaryDto
{
    public string CollectorName { get; set; }
    public decimal? Amount { get; set; }
    public List<PaymentSummaryListByDateDto> PaymentSummaryListByDate { get; set; } = new();
}
public class PaymentSummaryListByDateDto
{
    public decimal? Amount { get; set; }
    public string PaymentDate { get; set; }
    public List<PaymentSummaryListDto> PaymentSummaryList { get; set; } = new();
}

public class PaymentSummaryListDto
{
    public string LendingNumber { get; set; }
    public string CustomerName { get; set; }
    public decimal? PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }
}