namespace MicroFinancing.DataTransferModel;

public sealed class BatchDto
{
    public string Name { get; set; }
    public decimal Amount { get; set; }
    public int Participants { get; set; }
    public string Term { get; set; }
    public string AdministeringUser { get; set; }
    public int TotalAddedParticipants { get; set; }
}