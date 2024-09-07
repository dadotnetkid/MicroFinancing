using System.Reflection;

using MicroFinancing.MobileApp.Providers;
using MicroFinancing.MobileApp.Services.Client;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Logging;

namespace MicroFinancing.MobileApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomAuthenticationStateProvider>());

            builder.Services.AddSingleton<HttpClient>(_ => new HttpClient()
            {
                BaseAddress = new Uri("https://api-ccc.interworx.app:8001/")
            });

            var types = Assembly.GetExecutingAssembly()
                                .GetTypes()
                                .Where(c => c.BaseType == typeof(BaseApiClient))
                                .ToList();

            foreach (var type in types)
            {
                var interfaceType = type.GetInterface($"I{type.Name}");
                builder.Services.AddSingleton(interfaceType, type);
            }




            return builder.Build();
        }
    }
}
