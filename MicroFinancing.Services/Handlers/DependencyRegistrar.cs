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
        public static IServiceCollection RegisterReportHandlers(this IServiceCollection services)
        {
            services.AddScoped<ReportHandler>();

            services.AddScoped<PaymentSummaryReportHandler>();

            services.AddScoped<IEnumerable<IReportHandler>>(sp => new List<IReportHandler>()
            {
                sp.GetRequiredService<PaymentSummaryReportHandler>()
            });
            return services;
        }

    }
}