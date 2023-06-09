using MauiApp1.Data;
using MauiApp1.Services;
using Microsoft.AspNetCore.Components.WebView.Maui;
using SQLite;

namespace MauiApp1
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

    
            builder.Services.AddScoped<SQLiteAsyncConnection>(sp =>
            {
                var dbPath =
                    Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                        @"WeatherForecasts.db");
                var sql = new SQLiteAsyncConnection(dbPath);
                sql.CreateTableAsync<WeatherForecast>();
                return sql;
            });
            builder.Services.AddScoped<WeatherForecastService>();
            return builder.Build();
        }
    }
}