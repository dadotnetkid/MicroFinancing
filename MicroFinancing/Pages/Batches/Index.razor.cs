using MicroFinancing.DataTransferModel;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor.Grids;

namespace MicroFinancing.Pages.Batches;

public sealed partial class Index
{
    private AddBatch addBatchRef;
    public SfGrid<BatchDto> BatchGrid { get; set; }

    private void OnAddClick(MouseEventArgs arg)
    {
        addBatchRef.Show();
    }

    private async Task OnSuccess()
    {
        await BatchGrid.Refresh();
    }
}