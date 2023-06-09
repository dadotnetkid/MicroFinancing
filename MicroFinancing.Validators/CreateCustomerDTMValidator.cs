using FluentValidation;
using MicroFinancing.DataTransferModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinancing.Validators;

public sealed class CreateCustomerDTMValidator : AbstractValidator<CreateCustomerDTM>
{
    public CreateCustomerDTMValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage(x => $"{nameof(x.FirstName)} should not be empty");
        RuleFor(x => x.LastName).NotEmpty().WithMessage(x => $"{nameof(x.LastName)} should not be empty");
        RuleFor(x => x.Address).NotEmpty().WithMessage(x => $"{nameof(x.Address)} should not be empty");
        RuleFor(x => x.DateOfBirth).NotEmpty().WithMessage(x => $"{nameof(x.DateOfBirth)} should not be empty");
    }
}