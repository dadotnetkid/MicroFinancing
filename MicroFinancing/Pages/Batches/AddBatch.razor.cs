using Blazored.FluentValidation;
using MediatR;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Services.Handlers.BatchCommands;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MicroFinancing.Pages.Batches;

public partial class AddBatch
{
    [Inject] private IMediator _mediator { get; set; }
    private FluentValidationValidator fluentValidator;
    private bool visibility;
    private AddBatchDto model { get; set; } = new();

    [Parameter] public EventCallback<bool> OnSuccess { get; set; }

    private async Task OnBtnSubmitClick()
    {
        /*var valid = await fluentValidator.ValidateAsync();

        if (!valid)
        {
            return;
        }*/

        await _mediator.Send(new AddBatchCommand()
        {
            Batch = model
        });

        await OnSuccess.InvokeAsync(true);
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