using Microsoft.Extensions.DependencyInjection;

namespace MicroFinancing.Services.Handlers
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection RegisterMediatR(this IServiceCollection services)
        {
            services.AddMediatR(c =>
            {
                c.RegisterServicesFromAssemblies(typeof(DependencyRegistrar).Assembly);
            });
            return services;
        }

    }
}