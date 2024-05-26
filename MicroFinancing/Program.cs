using System.Diagnostics;
using FluentValidation;
using MicroFinancing.Areas.Identity;
using MicroFinancing.DataTransferModel;
using MicroFinancing.Entities;
using MicroFinancing.Interfaces.Repositories;
using MicroFinancing.Repositories;
using MicroFinancing.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;
using Syncfusion.Blazor.Data;
using System.Net;
using MicroFinancing.Core.Common;
using MicroFinancing.Interfaces.Services;
using MicroFinancing.Providers;
using MicroFinancing.Validators;
using MicroFinancing.Components;
using DevExpress.Blazor.Reporting;
using MicroFinancing.Services.Handlers;

var builder = WebApplication.CreateBuilder(args);

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(builder.Configuration.GetValue<string>("Syncfusion:Licensing"));

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MFDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Transient);


builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<MFDbContext>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ClaimsPrincipalFactory>();


builder.Services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSyncfusionBlazor();

/*Registration of Services*/
builder.Services.AddServices();
builder.Services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

/*Registration of Validators*/
builder.Services.AddValidators();

/*Registration of Component*/
builder.Services.AddComponents();

builder.WebHost.UseWebRoot("wwwroot");
builder.WebHost.UseStaticWebAssets();

builder.Services.AddDevExpressBlazor();
builder.Services.AddDevExpressServerSideBlazorReportViewer();

/*Registration of Policy*/
builder.Services.AddAuthorization(options =>
{
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


/*Registration of Singleton*/
builder.Services.AddSingleton(x => new ClaimsValueModel());
builder.Services.AddControllers();

builder.Services.RegisterMediatR();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
