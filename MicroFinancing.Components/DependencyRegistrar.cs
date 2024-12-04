using MicroFinancing.Components.ToastsComponent;
using Microsoft.Extensions.DependencyInjection;

using MicroFinancing.Components.DialogComponent;
using Syncfusion.Blazor.Popups;

namespace MicroFinancing.Components
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddComponents(this IServiceCollection services)
        {
            services.AddSingleton<IDialogService, DialogComponentService>();
            services.AddSingleton<IToasts, ToastComponentService>();

            services.AddScoped<SfDialogService>();
            return services;
        }
    }
}
