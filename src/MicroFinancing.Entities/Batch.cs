using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroFinancing.Entities;

public sealed class Batch : BaseEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override long Id { get; set; }
    public string AdministratingUserId { get; set; }
    public long TermId { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public int Participants { get; set; }
    public DateTimeOffset? StartAt { get; set; }

    public Term? Term { get; set; }
    public ICollection<BatchInCustomer> Customers { get; set; } = new HashSet<BatchInCustomer>();
    public ApplicationUser AdministratingUser { get; set; }
}
