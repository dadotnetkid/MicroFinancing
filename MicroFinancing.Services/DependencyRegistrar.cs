using MicroFinancing.Interfaces.Services;
using MicroFinancing.Providers;
using MicroFinancing.Repositories;
using MicroFinancing.Services.Handlers;
using MicroFinancing.WebAssembly.Services.Adaptors;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MicroFinancing.Services;

public static class DependencyRegistrar
{
    public static IServiceCollection AddBlazorServices(this IServiceCollection services)
    {
        //Repository
        services.AddTransient(typeof(IRepository<,>), typeof(BaseRepository<,>));

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
        services.AddTransient<PaymentApprovalAdaptor>();
        services.AddTransient<PaymentSummaryAdaptor>();

        //Transient
        services.AddTransient<IUserService, BlazorUserService>();
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<IPermissionService, PermissionService>();
        services.AddTransient<IPaymentService, PaymentService>();
        services.AddTransient<ILendingService, LendingService>();
        services.AddTransient<IReportingService, ReportingService>();
        services.AddTransient<IDashboardService, DashboardService>();
        services.AddTransient<ISecurityService, SecurityService>();
        services.AddTransient<IBatchService, BatchService>();
        services.AddTransient<ITermService, TermService>();
        services.AddTransient<ISmsService, SmsApiService>();

        services.AddTransient<ICurrentUser, WebAssembly.Services.CurrentUser>();

        services.AddTransient<ReConstructHandler>();

        //Scopes
        services
            .AddTransient<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
        services.AddTransient<IUserClaimsPrincipalFactory<ApplicationUser>, ClaimsPrincipalFactory>();

        //Singleton
        services.AddSingleton(x => new ClaimsValueModel());

        return services;
    }

    public static IServiceCollection AddApiServices(this IServiceCollection services)
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
        services.AddTransient<PaymentApprovalAdaptor>();

        //Transient
        services.AddTransient<IUserService, ApiUserService>();
        services.AddTransient<ICustomerService, CustomerService>();
        services.AddTransient<IPermissionService, PermissionService>();
        services.AddTransient<IPaymentService, PaymentService>();
        services.AddTransient<ILendingService, LendingService>();
        services.AddTransient<IReportingService, ReportingService>();
        services.AddTransient<IDashboardService, DashboardService>();
        services.AddTransient<ISecurityService, SecurityService>();
        services.AddTransient<IBatchService, BatchService>();
        services.AddTransient<ITermService, TermService>();
        services.AddTransient<ISmsService, SmsService>();

        services.AddTransient<ICurrentUser, CurrentUser>();

        services.AddTransient<ReConstructHandler>();

        //Scopes
        services
            .AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
        services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ClaimsPrincipalFactory>();

        //Singleton
        services.AddSingleton(x => new ClaimsValueModel());

        return services;
    }

    public static IServiceCollection AddPolicy(this IServiceCollection services)
    {
        /*Registration of Policy*/
        services.AddAuthorization(options =>
        {
            //Administrator
            options.AddPolicy(ClaimsConstant.Administrator,
                              policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Administrator));

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

            //Manage Permission
            options.AddPolicy(ClaimsConstant.Roles.Manage,
                              policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Roles.Manage));

            //View All Customer

            options.AddPolicy(ClaimsConstant.Customer.ViewAllCustomer,
                              policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Customer.ViewAllCustomer));

            //Manage Customer
            options.AddPolicy(ClaimsConstant.Customer.Manage,
                              policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Customer.Manage));

            //View Customer
            options.AddPolicy(ClaimsConstant.Customer.View,
                              policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Customer.View));

            //Add Customer
            options.AddPolicy(ClaimsConstant.Customer.Add,
                              policy => policy.RequireClaim(ClaimsConstant.ClaimType, ClaimsConstant.Policy.Customer.Add));

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
                              policy => policy.RequireClaim(ClaimsConstant.ClaimType,
                                                            ClaimsConstant.Policy.Customer.OverridePayment));

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
