using MicroFinancing.Core.Enumeration;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MicroFinancing.DataTransferModel;

public class PaymentBaseDTM
{
    public long Id { get; set; }
    public long CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public virtual PaymentEnum.PaymentType PaymentType { get; set; }
    public decimal? PaymentAmount { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    public bool Override { get; set; }
    public virtual string Reason { get; set; }
    public virtual DateTime PaymentDate { get; set; } = DateTime.Now;
    public virtual string? CreatedBy { get; set; }
    public virtual string? CreatedByUserId { get; set; }
}

public sealed class PaymentGridDTM : PaymentBaseDTM
{
    
}
public  class CreatePaymentDTM : PaymentBaseDTM
{
    public override PaymentEnum.PaymentType PaymentType { get; set; } = PaymentEnum.PaymentType.Cash;
    public override DateTime PaymentDate { get; set; } = DateTime.Now;
}
