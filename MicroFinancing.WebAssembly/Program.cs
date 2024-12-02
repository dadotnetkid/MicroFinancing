using MicroFinancing.Components.DialogComponent;
using MicroFinancing.WebAssembly;
using MicroFinancing.WebAssembly.Services;
using MicroFinancing.WebAssembly.Services.Adaptors;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
SyncfusionLicenseProvider.RegisterLicense(
    "MzYwNDE1MUAzMjM3MmUzMDJlMzBWSEZiQU5Oa2lQd2g2cGx5ZjQrblJqK3RWU3FCZEJDZXZTMG16L1BwRytBPQ==");

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IDialogService, DialogComponentService>();
builder.Services.AddScoped<IToasts, ToastComponentService>();

builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) })
    .AddApiClients()
    .AddCurrentUserService();

builder.Services.AddDataAdaptors();

builder.Services.AddSyncfusionBlazor();

await builder.Build()
    .RunAsync();