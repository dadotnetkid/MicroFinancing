namespace MicroFinancing.DataTransferModel;

public sealed class AddBatchDto
{
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public int Participants { get; set; }
    public long Term { get; set; }
    public string AdministeringUser { get; set; }
    public DateTimeOffset? StartAt { get; set; }
}