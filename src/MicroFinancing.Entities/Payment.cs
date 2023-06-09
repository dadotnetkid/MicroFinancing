using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MediatR;
using MicroFinancing.Core.Enumeration;

namespace MicroFinancing.Entities;

public sealed class Payment:IRequest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public PaymentEnum.PaymentType PaymentType { get; set; }
    public decimal? PaymentAmount { get; set; }
    public bool Override { get; set; }
    public string Reason { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime PaymentDate { get; set; }
    public string CreatedBy { get; set; }
    public string Attachment { get; set; }
    public long LendingId { get; set; }

    public Customers Customers { get; set; }

    public ApplicationUser CreatedByUser { get; set; }
    public Lending Lending { get; set; }
    
}