namespace MicroFinancing.DataTransferModel;

public sealed class ParticipantsInBatchDto
{
    public long Id { get; set; }
    public int Index { get; set; }
    public long CustomerId { get; set; }
    public string Name { get; set; }
}