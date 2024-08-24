using MicroFinancing.Core.Common;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using MicroFinancing.Interfaces.Services;
using MicroFinancing.Providers;
using MicroFinancing.Repositories;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MicroFinancing.Services;

public static class DependencyRegistrar
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        //Repository
        services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));

        //Scoped Adaptor
        services.AddTransient(typeof(BaseAdaptor<,>));
        services.AddTransient(typeof(UserAdaptor));
        services.AddTransient(typeof(CustomerAdaptor));
        services.AddTransient(typeof(PermissionAdaptor));
        services.AddTransient(typeof(PaymentAdaptor));
        services.AddTransient(typeof(LendingAdaptor));
        services.AddTransient(typeof(LendingSummaryAdaptor));
        services.AddTransient(typeof(CollectionSummaryReportAdaptor));
        services.AddTransient(typeof(CustomerByCollectorSummaryReportAdaptor));
        services.AddTransient(typeof(CustomerComboBoxAdaptor));
        services.AddTransient<BatchAdaptor>();
        services.AddTransient<TermAdaptor>();
        services.AddTransient<ParticipantInBatchAdaptor>();

        //Transient
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<IPermissionService, PermissionService>();
        services.AddTransient<IPaymentService, PaymentService>();
        services.AddTransient<ILendingService, LendingService>();
        services.AddTransient<IReportingService, ReportingService>();
        services.AddTransient<IDashboardService, DashboardService>();
        services.AddTransient<ISecurityService, SecurityService>();
        services.AddTransient<IBatchService, BatchService>();
        services.AddTransient<ITermService, TermService>();

        services.AddTransient<ICurrentUser, CurrentUser>();

        //Scopes
        services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
        services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ClaimsPrincipalFactory>();

        //Singleton
        services.AddSingleton(x => new ClaimsValueModel());
        return services;

    }

    public static IServiceCollection AddPolicy(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
         {

             //Administrator
             options.AddPolicy(ClaimsConstant.Administrator,
                               policy => policy.RequireClaim(ClaimsConstant.ClaimType, new []{ ClaimsConstant.Administrator }));
             //View Permission
             options.AddPolicy(ClaimsConstant.Users.View,
                 policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Users.View));
             //Manage User
             options.AddPolicy(ClaimsConstant.Users.Manage,
                 policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Users.Manage));

             options.AddPolicy(ClaimsConstant.Roles.View,
                 policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Roles.View));
             //Manage Permission
             options.AddPolicy(ClaimsConstant.Roles.Manage,
                 policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Roles.Manage));


             //Manage Customer
             options.AddPolicy(ClaimsConstant.Customer.Manage,
                 policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Customer.Manage));
             //View Customer
             options.AddPolicy(ClaimsConstant.Customer.View,
                 policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Customer.View));
             //Print SOA
             options.AddPolicy(ClaimsConstant.Customer.Print,
                 policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Customer.Print));
             //Print Manage Loan
             options.AddPolicy(ClaimsConstant.Customer.ManageLoan,
                 policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Customer.ManageLoan));
             //Print Add Loan
             options.AddPolicy(ClaimsConstant.Customer.AddLoan,
                 policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Customer.AddLoan));
             //Print Manage Payment
             options.AddPolicy(ClaimsConstant.Customer.ManagePayment,
                 policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Customer.ManagePayment));
             //Print Override Payment
             options.AddPolicy(ClaimsConstant.Customer.OverridePayment,
                 policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Customer.OverridePayment));
             //Print Add Loan
             options.AddPolicy(ClaimsConstant.Customer.AddPayment,
                 policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Customer.AddPayment));
             //Print Set Flag
             options.AddPolicy(ClaimsConstant.Customer.SetFlag,
                 policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Customer.SetFlag));
             //Print Print
             options.AddPolicy(ClaimsConstant.Customer.Print,
                 policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Customer.Print));
         });
        return services;
    }
}