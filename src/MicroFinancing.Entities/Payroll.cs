namespace MicroFinancing.Entities;

public sealed class Payroll : BaseEntity<long>
{
    public int Month { get; set; }
    public ICollection<PayrollItem> PayrollItems = new HashSet<PayrollItem>();
}
