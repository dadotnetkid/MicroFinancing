using MicroFinancing.Infrastructure.Enums;

namespace MicroFinancing.DataTransferModel;

public sealed class TermDto
{
    public long Id { get; set; }
    public string Name { get; set; }
    public long Number { get; set; }
    public TermEnum TermEnum { get; set; }
}