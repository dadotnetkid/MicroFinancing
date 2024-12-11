using System.Net;
using System.Reflection;

using MicroFinancing.MobileApp.Providers;
using MicroFinancing.MobileApp.Services;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

using Syncfusion.Blazor;

namespace MicroFinancing.MobileApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder.Services.AddSyncfusionBlazor();

        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHFqVkNrXVNbdV5dVGpAd0N3RGlcdlR1fUUmHVdTRHRcQl5hT39adk1gUH5XdXc=;Mgo+DSMBPh8sVXJ1S0d+X1RPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSX1RcERqWXddeXZVTmM=;ORg4AjUWIQA/Gnt2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5QdENiUH9Wc31XR2lf;MTQ3NjYzN0AzMjMxMmUzMTJlMzMzNUVFRXlxb3hBZWEwa3IvZVZocWU3UjUvQm5PS0t3cHZTUTM5aFJmczJ0YzQ9;MTQ3NjYzOEAzMjMxMmUzMTJlMzMzNW1nb3RiTXVsL09Id2REV1NuSlQyQ1dDT0FPODRGWW1iN0wwREdhNjNsTGc9;NRAiBiAaIQQuGjN/V0d+XU9Hc1RDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31TdUdlWHZeeHdcRGdaUg==;MTQ3NjY0MEAzMjMxMmUzMTJlMzMzNWJGMEo0VlE0SXpkbU9FSEpZaWtveko0QysrN3hGT3VCcURzQlFjczZmTjA9;MTQ3NjY0MUAzMjMxMmUzMTJlMzMzNUlhMFlWTUtxYUNnUFdVcHB0bnJjWEZHSDdOZmlYRU90SEtUT2RUQnZkOTA9;Mgo+DSMBMAY9C3t2VFhhQlJBfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5QdENiUH9Wc31WQmBf;MTQ3NjY0M0AzMjMxMmUzMTJlMzMzNVgyVFIvYTBsNFBjME41dXFhSmFCeXNVR1ptMEV4WmRFV1ZETkJYdWNFbnM9;MTQ3NjY0NEAzMjMxMmUzMTJlMzMzNUJLb1Q4WDNVSVZ6bjVMVkhwN3BCMm9KdStOa3dsQVpVRnI0V1RPVnZQWVk9;MTQ3NjY0NUAzMjMxMmUzMTJlMzMzNWJGMEo0VlE0SXpkbU9FSEpZaWtveko0QysrN3hGT3VCcURzQlFjczZmTjA9");

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<CustomAuthenticationStateProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthenticationStateProvider>());

        builder.Services.AddSingleton<CookieContainer>();

        builder.Services.AddSingleton<HttpClientHandler>(sp =>
        {
            var container = sp.GetRequiredService<CookieContainer>();

            return new HttpClientHandler()
            {
                CookieContainer = container,
                UseCookies = true
            };
        });

        builder.Services.AddSingleton<HttpClient>(_ =>
        {
            var handler = _.GetRequiredService<HttpClientHandler>();

            return new HttpClient(handler)
            {
                BaseAddress = new Uri("https://staging-ccc.interworx.app/")
            };
        });

        var types = typeof(BaseApiClient)
                    .Assembly
                    .GetTypes()
                    .Where(c => c.BaseType == typeof(BaseApiClient))
                    .ToList();

        foreach (var type in types)
        {
            var interfaceType = type.GetInterface($"I{type.Name}");
            builder.Services.AddSingleton(interfaceType, type);
        }

        types = typeof(CustomerClientAdaptor)
                    .Assembly
                    .GetTypes()
                    .Where(c => c.BaseType == typeof(DataAdaptor))
                    .ToList();

        foreach (var type in types)
        {
            builder.Services.AddSingleton(type);
        }

        return builder.Build();
    }
}
