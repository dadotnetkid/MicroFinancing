using FluentValidation;

using MicroFinancing.DataTransferModel;

using Microsoft.Extensions.DependencyInjection;

namespace MicroFinancing.Validators;

public static class DependencyRegistrar
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {

        services.AddTransient<IValidator<ResetPasswordUserDTM>, ResetPasswordUserDTMValidator>();
        services.AddTransient<IValidator<CreateCustomerDTM>, CreateCustomerDTMValidator>();
        services.AddTransient<IValidator<EditCustomerDTM>, EditCustomerDTMValidator>();
        services.AddTransient<IValidator<CreatePaymentDTM>, CreatePaymentDTMValidator>();
        services.AddTransient<IValidator<CreateLendingDTM>, CreateLendingDTMValidator>();
        services.AddTransient<IValidator<EditLendingDTM>, EditLendingDTMValidator>();

        return services;
    }
}
