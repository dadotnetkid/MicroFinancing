namespace MicroFinancing.DataTransferModel;

public sealed class StatementofAccountDTM
{
    public string Collector { get; set; }
    public DateTime ReleaseDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal DailyPayment { get; set; }

    public decimal MoneyAmount { get; set; }
    public decimal ItemsAmount { get; set; }
    public decimal Interest { get; set; }
    public decimal TotalCredit { get; set; }
    public List<PaymentDateDTM> PaymentDates { get; set; } = new();
    public string CustomerName { get; set; }

    public decimal DSTax => (MoneyAmount + ItemsAmount / 200M) * 1.5M;
}

public sealed class PaymentDateDTM
{
    public DateTime PaymentDate { get; set; }
    public decimal AmountPaid { get; set; }
    public string Notes { get; set; }
    public bool IsApproved { get; set; }
    public string AmountPaidWithNotes => $"₱ {AmountPaid:n2} {Notes}";
}
