namespace MicroFinancing.DataTransferModel;

public sealed class ApplicationRoleDTM
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? NormalizedName { get; set; }
    public string? ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
}