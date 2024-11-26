using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroFinancing.Entities;

public sealed class InterestRate : BaseEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override long Id { get; set; }
    public decimal Rate { get; set; }
    public int Term { get; set; }
    public bool IsActive { get; set; }
}