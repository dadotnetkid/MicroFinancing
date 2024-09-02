using FluentValidation;
using MicroFinancing.DataTransferModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinancing.Validators
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateUpdateUserDTM>, CreateUpdateUserDTMValidator>();
            services.AddTransient<IValidator<ResetPasswordUserDTM>, ResetPasswordUserDTMValidator>();
            services.AddTransient<IValidator<CreateCustomerDTM>, CreateCustomerDTMValidator>();
            services.AddTransient<IValidator<EditCustomerDTM>, EditCustomerDTMValidator>();
            services.AddTransient<IValidator<CreatePaymentDTM>, CreatePaymentDTMValidator>();
            services.AddTransient<IValidator<CreateLendingDTM>, CreateLendingDTMValidator>();
            services.AddTransient<IValidator<EditLendingDTM>, EditLendingDTMValidator>();
            return services;

        }
    }
}
