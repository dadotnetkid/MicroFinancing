using FluentValidation;
using MicroFinancing.DataTransferModel;

namespace MicroFinancing.Validators;

public class ResetPasswordUserDTMValidator : AbstractValidator<ResetPasswordUserDTM>
{
    public ResetPasswordUserDTMValidator()
    {
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage(x => $"{nameof(x.Password)} should not be empty")
            .MinimumLength(8).WithMessage(x => $"{nameof(x.Password)} must minimum of 8 characters");
    }
}