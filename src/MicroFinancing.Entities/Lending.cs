using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroFinancing.Core.Enumeration;

namespace MicroFinancing.Entities;

public sealed class Lending : BaseEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override long Id { get; set; }

    public long CustomerId { get; set; }
    public LendingEnumeration.LendingCategory Category { get; set; }
    public decimal Amount { get; set; }

    [Required] public DateTime LendingDate { get; set; }

    [Required] public DateTime DueDate { get; set; }

    [Required] public string CreatedBy { get; set; }

    [Required] public DateTime CreatedAt { get; set; }
    

    public bool IsDeleted { get; set; }

    /// <summary>
    ///     READONLY
    /// </summary>
    public decimal Interest { get; set; }

    /// <summary>
    ///     READONLY
    /// </summary>
    public decimal TotalCredit { get; set; }

    public decimal ItemAmount { get; set; }
    public string Collector { get; set; }
    public bool IsActive { get; set; }
    public bool IsPaid { get; set; }
    public decimal InterestRate { get; set; } = 10;
    public ApplicationUser CollectorUser { get; set; }
    public Customers Customers { get; set; }
    public ICollection<Payment> Payments { get; set; }
    public int NumberOfDays { get; set; }
    public int PaymentDays { get; set; }
    public decimal? DailyDueAmount { get; set; }
    public LendingEnumeration.Duration Duration { get; set; }
}