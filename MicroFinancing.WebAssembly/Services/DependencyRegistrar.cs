using MicroFinancing.Interfaces.Services;
using MicroFinancing.Services;
using MicroFinancing.WebAssembly.Services.Adaptors;
using Syncfusion.Blazor;

namespace MicroFinancing.WebAssembly.Services;

public static class DependencyRegistrar
{
    public static IServiceCollection AddApiClients(this IServiceCollection services)
    {
        var types = typeof(BaseApiClient)
            .Assembly
            .GetTypes()
            .Where(c => c.BaseType == typeof(BaseApiClient))
            .ToList();

        foreach (var type in types)
        {
            var interfaceType = type.GetInterface($"I{type.Name}");
            services.AddScoped(interfaceType, type);
        }

        return services;
    }

    public static IServiceCollection AddDataAdaptors(this IServiceCollection services)
    {
        var types = typeof(_Imports)
            .Assembly
            .GetTypes()
            .Where(c => c.BaseType == typeof(DataAdaptor))
            .ToList();
        foreach (var type in types)
        {
            services.AddScoped(type);
        }

        return services;
    }

    public static IServiceCollection AddCurrentUserService(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUser, CurrentUser>();
        return services;
    }
}