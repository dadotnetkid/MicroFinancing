using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinancing.DataTransferModel;

public sealed class StatementofAccountDTM
{
    public string Collector { get; set; }
    public DateTime ReleaseDate { get; set; }
    public DateTime DueDate { get; set; }
    public decimal DailyPayment => TotalCredit / (DueDate - ReleaseDate).Days;

    public decimal MoneyAmount { get; set; }
    public decimal ItemsAmount { get; set; }
    public decimal Interest => (MoneyAmount + ItemsAmount) * 0.1M;
    public decimal TotalCredit => MoneyAmount + Interest + ItemsAmount;
    public List<PaymentDateDTM> PaymentDates { get; set; } = new();
    public string CustomerName { get; set; }
}

public sealed class PaymentDateDTM
{
    public DateTime PaymentDate { get; set; }
    public decimal AmountPaid { get; set; }
    public string Notes { get; set; }
    public string AmountPaidWithNotes => $"₱ {AmountPaid:n2} {Notes}";
}