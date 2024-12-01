using System.ComponentModel.DataAnnotations;

namespace MicroFinancing.DataTransferModel;

public sealed class PermissionGridDTM
{
    public string Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<string> Scopes { get; set; }
    public int Users { get; set; }
}

public sealed class CreateUpdatePermissionDTM
{
    public CreateUpdatePermissionDTM()
    {
    }

    public string? Id { get; set; }
    public string? Name { get; set; }
    public string[]? Scopes { get; set; }
    public int? Users { get; set; }
}

public sealed class ItemsGridDTM
{
    public long Id { get; set; }
    [MaxLength(255)]
    public string? Name { get; set; }
    public decimal? Price { get; set; }
}