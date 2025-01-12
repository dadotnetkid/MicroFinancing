using Blazored.FluentValidation;

using MicroFinancing.Core.Common;
using MicroFinancing.Core.Enumeration;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Interfaces.Services;

using Microsoft.AspNetCore.Components;

namespace MicroFinancing.Pages.Lendings.LendingForApproval;

public partial class AddLendingForApproval
{
    [Parameter] public EventCallback<bool> OnSubmitSuccess { get; set; }

    [Inject] private ILendingService lendingService { get; set; }

    [Inject] private ICustomerService CustomerService { get; set; }
    private bool visibility;
    private FluentValidationValidator? fluentValidator;
    private CreateLendingForApprovalDTM model = new();

    public void Show()
    {
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
            await lendingService.AddLendingForApproval(model);
            await OnSubmitSuccess.InvokeAsync(true);
            Hide();
        }
    }

    private void OnModalClosed()
    {
        visibility = false;
    }


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

    private void OnCustomerChange(ChangeEventArgs<long, CustomerGridDTM> obj)
    {
        var data = obj.ItemData;

        model.PreviousBalance = 0.0M;

        if (data is null)
        {
            return;
        }

        var balance = CustomerService.GetCustomerBalance(data.Id);

        model.PreviousBalance = balance;
    }
}
