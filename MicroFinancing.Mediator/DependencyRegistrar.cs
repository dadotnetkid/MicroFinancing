using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace MicroFinancing.Mediator
{
    public static class DependencyRegistrar
    {
        public static IServiceCollection RegisterMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(DependencyRegistrar));
            return services;
        }

    }
}