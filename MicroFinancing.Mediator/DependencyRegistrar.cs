using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MicroFinancing.Mediator
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