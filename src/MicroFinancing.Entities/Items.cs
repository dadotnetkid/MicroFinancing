using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroFinancing.Entities;

public sealed class Items
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public string SKU { get; set; }
    public string Barcode { get; set; }
    [MaxLength(255)]
    public string? Name { get; set; }
    public decimal? Price { get; set; }
}