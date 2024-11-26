
using MicroFinancing.Core.Enumeration;
using System.ComponentModel.DataAnnotations;

namespace MicroFinancing.DataTransferModel;

public class BaseLendingDTM
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public LendingEnumeration.LendingCategory Category { get; set; }
    public decimal Amount { get; set; } = 0;
    public decimal ItemAmount { get; set; } = 0;
    [Display(Name = "Lending Date")]
    [DisplayFormat(DataFormatString = "MM/dd/yyyy")]
    public DateTime? LendingDate { get; set; } = DateTime.Now;
    [Display(Name = "Duration")]
    public LendingEnumeration.Duration Duration { get; set; }

    [Display(Name = "Due Date")]
    [DisplayFormat(DataFormatString = "MM/dd/yyyy")]
    public DateTime? DueDate { get; set; } = Convert.ToDateTime(DateTime.Now.ToString("MM-dd-yyyy")).AddDays(40);
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
    [Display(Name = "Customer Name")]
    public string CustomerName { get; set; }
    public string? Collector { get; set; }
}
public sealed class LendingGridDTM : BaseLendingDTM
{
    public bool IsRestruct { get; set; }
    public bool IsPaid { get; set; }
    public long ParentId { get; set; }
    public decimal TotalCredit { get; set; }
    public bool IsActive { get; set; }
    public string LendingNumber { get; set; }
}
public sealed class LendingSummaryGridDTM : BaseLendingDTM
{
    [DisplayFormat(DataFormatString = "n2")]
    [Display(Name = "Total Balance")]
    public decimal? TotalBalance { get; set; }
}

public sealed class CreateLendingDTM : BaseLendingDTM
{

}

public sealed class EditLendingDTM : BaseLendingDTM
{

}