using FluentValidation;

using MicroFinancing.DataTransferModel;

namespace MicroFinancing.Validators;

public sealed class EditLendingDTMValidator : AbstractValidator<EditLendingDTM>
{
    public EditLendingDTMValidator()
    {
        When(x => x.Category == Core.Enumeration.LendingEnumeration.LendingCategory.Both, () =>
        {
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage(x => $"Amount should not be zero");
            RuleFor(x => x.ItemAmount).GreaterThan(0).WithMessage(x => $"Item Amount should not be zero");
        });
        When(x => x.Category == Core.Enumeration.LendingEnumeration.LendingCategory.Cash, () =>
        {
            RuleFor(x => x.Amount).GreaterThan(0).WithMessage(x => $"Amount should not be zero");
        });
        When(x => x.Category == Core.Enumeration.LendingEnumeration.LendingCategory.Item, () =>
        {
            RuleFor(x => x.ItemAmount).GreaterThan(0).WithMessage(x => $"Item Amount should not be zero");
        });

        RuleFor(x => x.CustomerId).NotEmpty().WithMessage(x => $"Customer should not be empty");
        RuleFor(x => x.Category).NotEmpty().WithMessage(x => $"{nameof(x.Category)} should not be empty");
        RuleFor(x => x.LendingDate).NotEmpty().WithMessage(x => $"{nameof(x.LendingDate)} should not be empty");
        RuleFor(x => x.DueDate).NotEmpty().WithMessage(x => $"{nameof(x.DueDate)} should not be empty");
        RuleFor(x => x.Collector).NotEmpty().WithMessage(x => $"{nameof(x.Collector)} should not be empty");
    }
}
