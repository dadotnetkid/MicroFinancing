using Blazored.FluentValidation;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;
using Microsoft.AspNetCore.Components;

namespace MicroFinancing.Pages.Customers.Details;

public partial class EditLending
{
    [CascadingParameter(Name = nameof(MainPage))] public Index? MainPage { get; set; }
    [Parameter] public Lending? Lending { get; set; }
    [Parameter] public EventCallback OnSubmitSuccess { get; set; }
    private bool visibility;
    private FluentValidationValidator? fluentValidator;
    private EditLendingDTM model = new();
    [Inject] private ILendingService lendingService { get; set; }

    public void Show(long id)
    {
        model = lendingService.GetLendingDetailsForEdit(id);
        visibility = true;
        StateHasChanged();
    }

    public void Hide()
    {
        visibility = false;
        model = new();
        StateHasChanged();
    }

    private void OnBtnCancelClick()
    {
        Hide();
    }

    private async Task OnBtnSubmitClick()
    {
        var validate = await fluentValidator?.ValidateAsync(x => { })!;
        if (validate)
        {
            await lendingService.EditLending(model);
            await OnSubmitSuccess.InvokeAsync();
            Hide();
        }
    }


    private void OnModalClosed()
    {
        visibility = false;
    }
}
