using FluentValidation;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MicroFinancing.Validators
{
    public class CreateUpdateUserDTMValidator : AbstractValidator<CreateUpdateUserDTM>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateUpdateUserDTMValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
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
                RuleFor(x => x.UserName).Must(CheckIfUserNameExist).WithMessage(x => $"{nameof(x.UserName)} '{x.UserName}' is already exist");
            });


            RuleFor(x => x.FirstName).NotEmpty().WithMessage(x => $"{nameof(x.FirstName)} should not be empty");
            RuleFor(x => x.LastName).NotEmpty().WithMessage(x => $"{nameof(x.LastName)} should not be empty");
            RuleFor(x => x.Email).NotEmpty().WithMessage(x => $"{nameof(x.Email)} should not be empty");
            RuleFor(x => x.UserName).NotEmpty().WithMessage(x => $"{nameof(x.UserName)} should not be empty");
        }

        private bool CheckIfUserNameExist(CreateUpdateUserDTM arg1, string? arg2)
        {
            return !_userManager.Users.Any(x => x.UserName == arg1.UserName);
        }
    }
}