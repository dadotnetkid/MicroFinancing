using MicroFinancing.DataTransferModel;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;

namespace MicroFinancing.Pages.Reports
{
    public partial class CollectionSummary
    {
        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }
        public string Collector { get; set; }
        public Query QueryData { get; set; } = new();

        private SfGrid<CollectionSummaryReportDTM>? customerGrid;
        private string[] GroupBy => [ "PaymentDate"];
        private void Search()
        {
            QueryData.AddParams(nameof(DateFrom),DateFrom);
            QueryData.AddParams(nameof(DateTo), DateTo);
            QueryData.AddParams(nameof(Collector), Collector);

            customerGrid?.Refresh();
        }
    }
}
