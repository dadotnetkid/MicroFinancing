using MicroFinancing.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MicroFinancing.DataMigrations
{
    public class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                  .SetBasePath(Directory.GetCurrentDirectory())
                  .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<MFDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("MicroFinancing.DataMigrations"));
        }
    }

    public class MFDbContextFactory : IDesignTimeDbContextFactory<MFDbContext>
    {
        public MFDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            var configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<MFDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("MicroFinancing.DataMigrations"));

            return new MFDbContext(optionsBuilder.Options);
        }
    }
}
