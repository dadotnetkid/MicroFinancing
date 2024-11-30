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
using Hangfire;

using MicroFinancing;
using MicroFinancing.Infrastructure;
using MicroFinancing.Services.Handlers;
using Serilog.Events;
using Serilog;

using ILogger = Serilog.ILogger;

var builder = WebApplication.CreateBuilder(args);

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(
    builder.Configuration.GetValue<string>("Syncfusion:Licensing"));

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MFDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Transient);

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Password.RequireDigit = false;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.Password.RequiredUniqueChars = 0;
    })
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<MFDbContext>();

builder.Services.AddTransient<IUserClaimsPrincipalFactory<ApplicationUser>, ClaimsPrincipalFactory>();


builder.Services.AddRazorComponents()
       .AddInteractiveServerComponents()
       .AddInteractiveWebAssemblyComponents();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddSyncfusionBlazor();

/*Registration of Services*/
builder.Services.AddBlazorServices();
builder.Services
    .AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

/*Registration of Validators*/
builder.Services.AddValidators();

/*Registration of Component*/
builder.Services.AddComponents();

builder.WebHost.UseWebRoot("wwwroot");
builder.WebHost.UseStaticWebAssets();

builder.Services.AddDevExpressBlazor();
builder.Services.AddDevExpressServerSideBlazorReportViewer();

/*Registration of Policy*/
builder.Services.AddPolicy();

/*Registration of Singleton*/
builder.Services.AddSingleton(x => new ClaimsValueModel());
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument();
builder.Services.RegisterMediatR();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddHttpClient("HttpClientWithSSLUntrusted",
                               client =>
                               {
                                   client.BaseAddress = new Uri("http://sms-api.interworx.app");


                               }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
                               {
                                   ClientCertificateOptions = ClientCertificateOption.Manual,
                                   ServerCertificateCustomValidationCallback =
        (httpRequestMessage, cert, cetChain, policyErrors) =>
        {
            return true;
        }
                               });

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

builder.Services.AddLogging(b =>
{
    var path = Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "Logs",
                            $"logs-{DateTime.UtcNow:dd-MM-yyyy}.txt");
    var logger = new LoggerConfiguration()
                 .MinimumLevel.Information()
                 .WriteTo.File(path, rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Information,
                               outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                               retainedFileTimeLimit: new TimeSpan(365, 0, 0, 0),
                               fileSizeLimitBytes: 10240000)
                 .CreateLogger();
    b.AddSerilog(logger);
}).AddSingleton<ILogger>(sp => new LoggerConfiguration()
                               .MinimumLevel.Information()
                               .CreateLogger());


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped(sp =>
{
    var httpContextAccessor = sp.CreateScope()
      .ServiceProvider.GetRequiredService<IHttpContextAccessor>();

    var httpContext = httpContextAccessor.HttpContext;

    return new HttpClient
    {
        BaseAddress = new Uri($"{httpContext.Request.Scheme}://{httpContext.Request.Host}")
    };
});

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

app.UseOpenApi();
app.UseSwaggerUi();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAntiforgery();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode()
   .AddInteractiveWebAssemblyRenderMode()
   .AddAdditionalAssemblies(typeof(MicroFinancing.WebAssembly._Imports).Assembly);

app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "Sample Jobs",
    Authorization = new[]
    {
        new  HangfireAuthorizationFilter("admin")
    }
});

app.MapAdditionalIdentityEndpoints();

//RecurringJob.AddOrUpdate<ReConstructHandler>("ReconstructLending", x => x.Handle(), Cron.Daily);
//await BuilderFix.Run(app.Services);
app.Run();