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
using MicroFinancing.Infrastructure;
using MicroFinancing.Services.Handlers;
using NuGet.Protocol.Core.Types;

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
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ClaimsPrincipalFactory>();


builder.Services.AddScoped(typeof(IRepository<,>), typeof(BaseRepository<,>));
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

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

builder.Services.RegisterMediatR();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddHangfire(configuration => configuration
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

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
app.UseHangfireDashboard("/hangfire", new DashboardOptions
{
    DashboardTitle = "Sample Jobs",
    Authorization = new[]
    {
        new  HangfireAuthorizationFilter("admin")
    }
});

//RecurringJob.AddOrUpdate<ReConstructHandler>("ReconstructLending", x => x.Handle(), Cron.Daily);
//await TestClass.Test(app.Services);
app.Run();

public class TestClass
{
    public static async Task Test(IServiceProvider services)
    {
        var _repository = services.CreateScope().ServiceProvider.GetService<IRepository<Customers, long>>();
        var lending = services.CreateScope().ServiceProvider.GetService<IRepository<Lending, long>>();

        _repository.Database.BeginTransaction();

        var customersList = _repository.Entity
                                       .Where(c => c.Lending.Any())
                                       .Where(c => !c.IsDeleted)
                                       .Where(c => c.Lending.Any(l => !l.IsDeleted))
                                       .Where(c => c.Lending.All(l => l.IsPaid))
                                       .Include(c => c.Lending).ToList();

        foreach (var c in customersList)
        {
            var customer = c.FullName;
            var id = c.Id;
            if (c.Lending.All(e => e.IsPaid))
            {
                var lendingres = c.Lending.LastOrDefault();

                lendingres.IsActive = true;
                lendingres.IsPaid = false;
                await _repository.SaveChangesAsync();

            }

        }


        await _repository.Database.CommitTransactionAsync();
    }

    private static List<long> ListOfIds = new();
    private static IRepository<Lending, long>? repository;

    private static void GetLatestLoan(long id = 0)
    {
        var qry = repository.Entity.Where(c => c.ParentLendingId != 0);

        if (id > 0)
        {
            qry = qry.Where(c => c.ParentLendingId == id);
        }

        var res = qry.ToList();

        foreach (var _res in res)
        {
            var ___ = new Lending();

            if (id == 0)
            {
                ___ = repository.Entity.Where(c => c.Id == _res.ParentLendingId)
                                .FirstOrDefault();
            }
            else
            {
                ___ = repository.Entity.Where(c => c.ParentLendingId == _res.Id)
                                .FirstOrDefault();
            }

            if (___.IsRestruct)
            {
                GetLatestLoan(___.Id);
                return;
            }
            else
            {
                _res.IsPaid = false;
                _res.IsActive = true;

            }
        }
    }
}