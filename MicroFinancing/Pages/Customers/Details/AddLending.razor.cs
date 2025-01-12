using Blazored.FluentValidation;
using Microfinancing.WebApiClient.Services.Client;
using MicroFinancing.Core.Common;
using MicroFinancing.Core.Enumeration;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;

using Microsoft.AspNetCore.Components;

namespace MicroFinancing.Pages.Customers.Details;

public partial class AddLending
{
    [CascadingParameter(Name = nameof(MainPage))] public Index? MainPage { get; set; }
    [Parameter] public Lending? Lending { get; set; }
    [Inject] private ILendingService lendingService { get; set; }
    [Inject] private ILendingClient LendingClient { get; set; }
    private bool visibility;
    private FluentValidationValidator? fluentValidator;
    private CreateLendingDTM model = new();

    public void Show()
    {
        model.CustomerId = MainPage.Id;
        visibility = true;
        this.Durations = typeof(LendingEnumeration.Duration).GetDropDown<LendingEnumeration.Duration>();
        StateHasChanged();
    }

    public List<GenericDropItem<LendingEnumeration.Duration>> Durations { get; set; } = new();

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
            await lendingService.AddLending(model);
            await Lending?.RefreshGrid()!;
            await MainPage.GetDetails();
            Hide();
        }
    }


    private void OnModalClosed()
    {
        visibility = false;
    }

    /*private void OnDurationChange(ChangeEventArgs<LendingEnumeration.Duration, string> obj)
        {
        if (obj.Value == LendingEnumeration.Duration.Custom)
        {
        return;
        }

        var res = obj.Value.GetDefault<int>();

        model.DueDate = model.DueDate?.AddDays(res);
    }*/

    /*private void OnDurationChange(ChangeEventArgs<GenericDropItem, GenericDropItem> obj)
        {
        throw new NotImplementedException();
    }*/

    private void OnDurationChange(ChangeEventArgs<LendingEnumeration.Duration, GenericDropItem<LendingEnumeration.Duration>> obj)
    {
        if (obj.Value == LendingEnumeration.Duration.Custom)
        {
            return;
        }

        var res = obj.Value.GetDefault<int>();

        model.DueDate = Convert.ToDateTime(model.LendingDate?.ToShortDateString()).AddDays(res);
    }

    private void ChangeLendingDate(ChangedEventArgs<DateTime?> obj)
    {
        if (model.Duration == LendingEnumeration.Duration.Custom)
        {
            return;
        }

        var res = model.Duration.GetDefault<int>();

        model.DueDate = Convert.ToDateTime(model.LendingDate?.ToShortDateString()).AddDays(res);
    }
}
