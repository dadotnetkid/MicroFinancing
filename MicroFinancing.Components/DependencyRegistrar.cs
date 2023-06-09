using MicroFinancing.Components.ToastsComponent;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinancing.Components
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection AddComponents(this IServiceCollection services)
        {
            services.AddScoped<IToasts, ToastComponentService>();
            return services;
        }
    }
}
