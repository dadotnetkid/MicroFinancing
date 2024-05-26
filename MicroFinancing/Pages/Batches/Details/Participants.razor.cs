using MicroFinancing.DataTransferModel;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Data;
using Syncfusion.Blazor.Grids;

namespace MicroFinancing.Pages.Batches.Details;

public partial class Participants
{
    private Query queryData = new();
    [Parameter] public long BatchId { get; set; }
    public SfGrid<ParticipantsInBatchDto> ParticipantGridRef { get; set; }

    protected override void OnInitialized()
    {
        queryData.AddParams("BatchId", BatchId);
    }
}