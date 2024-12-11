using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroFinancing.Entities;

public sealed class Items : BaseEntity<long>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public override long Id { get; set; }
    public string SKU { get; set; }
    public string Barcode { get; set; }
    [MaxLength(255)]
    public string? Name { get; set; }

    //  public ICollection<ItemPrice> Prices { get; set; } = new HashSet<ItemPrice>();
}

public sealed class ItemPrice : BaseEntity<long>
{
    public long ItemId { get; set; }
    public long PriceId { get; set; }
    public bool IsActive { get; set; }

    [ForeignKey(nameof(PriceId))]
    public Price Price { get; set; }
    [ForeignKey(nameof(ItemId))]
    public Items Item { get; set; }
}
public sealed class Price : BaseEntity<long>
{
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public ICollection<ItemPrice> Items { get; set; } = new HashSet<ItemPrice>();
}