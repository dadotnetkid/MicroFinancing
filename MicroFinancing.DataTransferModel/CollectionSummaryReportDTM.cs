using System.ComponentModel.DataAnnotations;

namespace MicroFinancing.DataTransferModel;

public sealed class CollectionSummaryReportDTM
{
    public string Collector { get; set; }
    [Display(Name = "Encoded By")]
    public string EncodedBy { get; set; }
    [Display(Name = "Customer Name")]
    public string CustomerName { get; set; }
    [DisplayFormat(DataFormatString = "n2")]
    public decimal? PaymentAmount { get; set; }
    [DisplayFormat(DataFormatString = "MM/dd/yyyy")]
    public DateTime PaymentDate { get; set; }
}

public sealed class CustomersByCollectorSummaryDTM
{
    [Display(Name = "Customer Name")]
    public string CustomerName { get; set; }
}