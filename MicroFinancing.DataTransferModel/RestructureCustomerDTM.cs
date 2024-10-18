using MicroFinancing.Core.Enumeration;

namespace MicroFinancing.DataTransferModel;

public sealed class RestructureCustomerDTM 
{
    public decimal? Balance { get; set; }
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public string Collector { get; set; }
    public DateTime DueDate { get; set; }
    public decimal Amount { get; set; }
    public decimal ItemAmount { get; set; }
    public LendingEnumeration.LendingCategory Category { get; set; }
    public DateTime LendingDate { get; set; }
    public string PhoneNumber { get; set; }
    public string FullName { get; set; }
}
