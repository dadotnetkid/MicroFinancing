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
using System.Text;
using MicroFinancing.Core.Common;
using MicroFinancing.Interfaces.Services;
using MicroFinancing.Providers;
using MicroFinancing.Validators;
using MicroFinancing.Components;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using DevExpress.XtraCharts;
using Microsoft.Extensions.Options;
using DevExpress.Data.TreeList;
using MicroFinancing.Mediator;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzE4MTMxQDMyMzAyZTMyMmUzMFFhSjB6TThEUFgxV3JJWDBFVGZPZmJDdjg5cUVDQWRobzlKSXRwNFdpVUU9");

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MFDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Transient);


builder.Services.AddDatabaseDeveloperPageExceptionFilter();




builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });

/*Registration of Services*/
builder.Services.AddServices();


/*Registration of Validators*/
builder.Services.AddValidators();

/*Registration of Component*/
builder.Services.AddComponents();



builder.Services.AddOptions();
builder.Services.Configure<JwtSetting>(builder.Configuration.GetSection("Jwt"));


/*Registration of Singleton*/

builder.Services.AddControllers();
/*Registration of Authentication of Cookies*/
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = $"{JwtBearerDefaults.AuthenticationScheme}/{IdentityConstants.ApplicationScheme}";
    options.DefaultChallengeScheme = $"{JwtBearerDefaults.AuthenticationScheme}/{IdentityConstants.ApplicationScheme}";
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
}).AddCookie(IdentityConstants.ApplicationScheme, o =>
{
    o.ExpireTimeSpan = TimeSpan.FromMinutes(30); // optional
}).AddPolicyScheme($"{JwtBearerDefaults.AuthenticationScheme}/{IdentityConstants.ApplicationScheme}", $"{JwtBearerDefaults.AuthenticationScheme}/{IdentityConstants.ApplicationScheme}",
    options =>
    {
        options.ForwardDefaultSelector = context =>
        {
            // filter by auth type
            string authorization = context.Request.Headers[HeaderNames.Authorization];
            if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith(JwtBearerDefaults.AuthenticationScheme))
                return "Bearer";

            // otherwise always check for cookie auth
            return IdentityConstants.ApplicationScheme;
        };
    });

/*Registration of Policy*/
builder.Services.AddPolicy();
/*Registration of Identity*/
builder.Services
    .AddIdentityCore<ApplicationUser>()
    .AddRoles<ApplicationRole>()
    .AddDefaultTokenProviders()
    .AddDefaultUI()
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddSignInManager<SignInManager<ApplicationUser>>()
    .AddEntityFrameworkStores<MFDbContext>();
builder.Services.AddSession();

builder.Services.RegisterMediatR();
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

/*app.UseHttpsRedirection();*/

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
