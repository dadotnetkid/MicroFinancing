using MicroFinancing.Mobile.Data;
using MicroFinancing.Mobile.Services;
using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.DependencyInjection;
using SQLite;
using Syncfusion.Blazor;

namespace MicroFinancing.Mobile
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
#endif

            builder.Services.AddSingleton<WeatherForecastService>();
            builder.Services.AddScoped( sp =>
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "entities.db3");
                var db = new SQLiteAsyncConnection(dbPath);
           
                return db;
            });
            builder.Services.AddScoped<HttpClient>(sp =>
            {
                return new HttpClient()
                {
                    BaseAddress = new Uri("http://192.168.1.42:45458/")
                };
            });
            builder.Services.AddScoped<CustomerService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<SecurityService>();


            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NzE4MTMxQDMyMzAyZTMyMmUzMFFhSjB6TThEUFgxV3JJWDBFVGZPZmJDdjg5cUVDQWRobzlKSXRwNFdpVUU9");
            builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });
            return builder.Build();
        }
    }
}