using System.Diagnostics.CodeAnalysis;
using JosephHungerman.Data;
using JosephHungerman.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JosephHungerman.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(
                    configuration.GetConnectionString("JshStage"),
                    new MariaDbServerVersion(new Version(10, 3)));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
