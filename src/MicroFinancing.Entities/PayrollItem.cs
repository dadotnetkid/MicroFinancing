using System.ComponentModel.DataAnnotations.Schema;

namespace MicroFinancing.Entities;

public class PayrollItem : BaseEntity<long>
{
    public long PayrollId { get; set; }
    public string UserId { get; set; }
    public decimal BasicPay { get; set; }
    public decimal? Commission { get; set; }
    public decimal? Bonus { get; set; }



    [ForeignKey(nameof(PayrollId))]
    public Payroll Payroll { get; set; }
}
