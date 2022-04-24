using System.Diagnostics.CodeAnalysis;
using JosephHungerman.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JosephHungerman.Data.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(
                    configuration.GetConnectionString("JshStage"),
                    new MariaDbServerVersion(new Version(10, 3)));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<DbInitializer>();

            using var dbinit = services.BuildServiceProvider().GetRequiredService<ApplicationDbContext>();
            DbInitializer.SeedResumeData(dbinit).Wait();

            return services;
        }
    }
}
