using FluentValidation;
using MicroFinancing.DataTransferModel;

namespace MicroFinancing.Validators;

public sealed class CreatePaymentDTMValidator : AbstractValidator<CreatePaymentDTM>
{
    public CreatePaymentDTMValidator()
    {
        When(x => !x.Override, () =>
        {
            RuleFor(x=>x.PaymentAmount).GreaterThan(0).WithMessage(x => $"{nameof(x.PaymentAmount)} should not be zero");
        });
        When(x => x.Override, () =>
        {
            RuleFor(x=>x.Reason).NotEmpty().WithMessage( x=> $"{nameof(x.Reason)} should not be empty");
        });
        RuleFor(x => x.PaymentAmount).NotEmpty().WithMessage(x => $"{nameof(x.PaymentAmount)} should not be empty");
    }
}