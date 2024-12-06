using FluentValidation;

namespace MicroFinancing.WebAssembly.Validators
{
    public class CreateUpdateUserDTMValidator : AbstractValidator<CreateUpdateUserDTM>
    {

        public CreateUpdateUserDTMValidator()
        {
            When(x => !string.IsNullOrEmpty(x.UserId), () =>
            {
                When(x => !string.IsNullOrEmpty(x.Password), () =>
                {
                    RuleFor(x => x.Password)
                        .NotEmpty().WithMessage(x => $"{nameof(x.Password)} should not be empty")
                        .MinimumLength(8).WithMessage(x => $"{nameof(x.Password)} must minimum of 8 characters");
                });
            });
            When(x => string.IsNullOrEmpty(x.UserId), () =>
            {
                RuleFor(x => x.Password)
                    .NotEmpty().WithMessage(x => $"{nameof(x.Password)} should not be empty")
                    .MinimumLength(8).WithMessage(x => $"{nameof(x.Password)} must minimum of 8 characters");
             
            });


            RuleFor(x => x.FirstName).NotEmpty().WithMessage(x => $"{nameof(x.FirstName)} should not be empty");
            RuleFor(x => x.LastName).NotEmpty().WithMessage(x => $"{nameof(x.LastName)} should not be empty");
            RuleFor(x => x.Email).NotEmpty().WithMessage(x => $"{nameof(x.Email)} should not be empty");
            RuleFor(x => x.UserName).NotEmpty().WithMessage(x => $"{nameof(x.UserName)} should not be empty");
        }
    }
}