using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroFinancing.Entities;

public sealed class BatchInCustomer : BaseEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override long Id { get; set; }
    public int Index { get; set; }
    public long CustomerId { get; set; }
    public long BatchId { get; set; }

    [ForeignKey(nameof(BatchId))]
    public Batch Batch { get; set; }
    [ForeignKey(nameof(CustomerId))]
    public Customers Customers { get; set; }
}