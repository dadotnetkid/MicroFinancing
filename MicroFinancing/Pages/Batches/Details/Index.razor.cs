using MediatR;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Services.Handlers.BatchCommands;
using Microsoft.AspNetCore.Components;

namespace MicroFinancing.Pages.Batches.Details;

public partial class Index
{
    [Parameter] public long BatchId { get; set; }
    [Inject] private IMediator mediator { get; set; }
    private BatchDto model { get; set; }
    public Participants Participants { get; set; }
    public AddParticipant AddParticipantRef { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            model = await mediator.Send(new GetBatchCommand()
            {
                Id = BatchId
            });

            StateHasChanged();
        }
    }

    private void OnAddParticipant()
    {
        AddParticipantRef.Show();
    }

    private async Task OnSuccessCreateParticipant()
    {
        await Participants.ParticipantGridRef.Refresh();
    }
}