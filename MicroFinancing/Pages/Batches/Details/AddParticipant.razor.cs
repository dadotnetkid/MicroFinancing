using Blazored.FluentValidation;
using DevExpress.XtraRichEdit.Layout.Engine;
using MediatR;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Services.Handlers.BatchCommands;
using Microsoft.AspNetCore.Components;

namespace MicroFinancing.Pages.Batches.Details;

public partial class AddParticipant
{
    private FluentValidationValidator fluentValidator;
    private bool visibility { get; set; }

    private AddParticipantsInBatchDto model = new();
    [Parameter] public long BatchId { get; set; }
    [Parameter] public EventCallback<bool> OnSuccessCreateParticipant { get; set; }

    [Inject] private IMediator mediator { get; set; }

    private void OnModalClosed()
    {
        Hide();
    }

    private async Task OnBtnSubmitClick()
    {
        await mediator.Send(new AddParticipantInBatchCommand()
        {
            BatchId = BatchId,
            CustomerId = model.CustomerId
        });
        await OnSuccessCreateParticipant.InvokeAsync(true);
        Hide();
    }

    private void OnBtnCancelClick()
    {
        Hide();
    }

    public void Show()
    {
        visibility = true;

        StateHasChanged();
    }
    public void Hide()
    {
        visibility = false;
        StateHasChanged();
    }
}