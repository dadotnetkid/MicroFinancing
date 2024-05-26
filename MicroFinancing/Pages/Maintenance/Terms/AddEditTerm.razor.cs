using Blazored.FluentValidation;
using MediatR;
using MicroFinancing.Components.ToastsComponent;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Infrastructure.Common;
using MicroFinancing.Infrastructure.Enums;
using MicroFinancing.Interfaces.Services;
using MicroFinancing.Services.Handlers.AddTerm;
using Microsoft.AspNetCore.Components;

namespace MicroFinancing.Pages.Maintenance.Terms
{
    public partial class AddEditTerm
    {
        [CascadingParameter(Name = "MainPage")] Users.Index? MainPage { get; set; }
        private bool visibility;
        private FluentValidationValidator? fluentValidator;
        private TermDto term = new();
        [Inject] private IUserService? userService { get; set; }
        [Inject] private IToasts toasts { get; set; }
        [Inject] private IMediator? _mediator { get; set; }

        public void Show(TermDto? model = null)
        {
            visibility = true;

            TermTypes = EnumExtensions.ToDictionary<TermEnum>();
            if (model is not null)
            {
                term = model;
            }
            StateHasChanged();
        }

        public Dictionary<TermEnum, string> TermTypes { get; set; } = new();

        public void Hide()
        {
            visibility = false;
            term = new();
            StateHasChanged();
        }

        private void OnBtnCancelClick()
        {
            Hide();
        }

        private async Task OnBtnSubmitClick()
        {
            /*var validate = await fluentValidator?.ValidateAsync(x => { })!;
            if (!validate)
            {
                return;
            }*/

            term = await _mediator.Send(new AddTermCommand()
            {
                Term = term
            });
        }
    }
}
