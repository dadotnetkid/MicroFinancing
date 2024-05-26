using MicroFinancing.DataTransferModel;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;

namespace MicroFinancing.Pages.Batches.Details;

public partial class Participants
{
    private Query queryData = new();
    [Parameter] public long BatchId { get; set; }
    public SfGrid<LendingGridDTM> ParticipantGridRef { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            queryData.AddParams("BatchId", BatchId);
            await ParticipantGridRef.Refresh();
        }
    }
}