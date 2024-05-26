namespace MicroFinancing.DataTransferModel;

public sealed class BatchDetailDto
{
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public int Participants { get; set; }
    public int Term { get; set; }
    public string AdministeringUser { get; set; }
}