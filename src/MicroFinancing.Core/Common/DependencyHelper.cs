using Microsoft.Extensions.DependencyInjection;

namespace MicroFinancing.Core.Common;

public static class DependencyHelper
{
    public static IServiceProvider ServiceProvider { get; set; }
    public static void Instantiate(this IServiceProvider serviceProvider)
    {
        ServiceProvider ??= serviceProvider;
    }
    public static T GetRequiredService<T>()
    {
        return (T)ServiceProvider.CreateScope().ServiceProvider.GetRequiredService(typeof(T));
    }
}
