namespace MicroFinancing.Entities;

public class Audit
{
    public long Id { get; set; }
    public string TableName { get; set; }
    public string NewValue { get; set; }
    public string OldValue { get; set; }
}
