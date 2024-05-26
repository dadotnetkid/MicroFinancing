using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MicroFinancing.Infrastructure.Enums;

namespace MicroFinancing.Entities;

public class Term : BaseEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override long Id { get; set; }
    public string Name { get; set; }
    public long Number { get; set; }
    public TermEnum TermEnum { get; set; }
    public ICollection<Batch> Batch { get; set; } = new HashSet<Batch>();
}
