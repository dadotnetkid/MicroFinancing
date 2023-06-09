using MicroFinancing.Core.Enumeration;

namespace MicroFinancing.Mobile.DTM;

public class PaymentBaseDTM
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public string CustomerName { get; set; }
    public virtual PaymentEnum.PaymentType PaymentType { get; set; }
    public decimal? PaymentAmount { get; set; }
    public DateTime CreatedAt { get; set; }

    public bool Override { get; set; }
    public string Reason { get; set; }
    public virtual DateTime PaymentDate { get; set; } = DateTime.Now;
    public string CreatedBy { get; set; }
    public string CreatedByUserId { get; set; }
}

public sealed class PaymentGridDTM : PaymentBaseDTM
{
    
}
public sealed class CreatePaymentDTM : PaymentBaseDTM
{
    public override PaymentEnum.PaymentType PaymentType { get; set; } = PaymentEnum.PaymentType.Cash;
    public override DateTime PaymentDate { get; set; } = DateTime.Now;
}