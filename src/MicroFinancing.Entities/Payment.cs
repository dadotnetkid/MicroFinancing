using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MediatR;
using MicroFinancing.Core.Enumeration;

namespace MicroFinancing.Entities;

public sealed class Payment : BaseEntity<long>, IRequest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override long Id { get; set; }
    public long CustomerId { get; set; }
    public PaymentEnum.PaymentType PaymentType { get; set; }
    public decimal? PaymentAmount { get; set; }
    public bool Override { get; set; }
    public string Reason { get; set; }
    public DateTime PaymentDate { get; set; }
    public string Attachment { get; set; }
    public long LendingId { get; set; }

    public Customers Customers { get; set; }

    public Lending Lending { get; set; }

}