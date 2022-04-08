using System.Diagnostics.CodeAnalysis;
using JosephHungerman.Core.Options;
using JosephHungerman.Data;
using JosephHungerman.Data.Repositories;
using JosephHungerman.Helpers;
using JosephHungerman.Services;
using Microsoft.EntityFrameworkCore;

namespace JosephHungerman.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<Email>(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production"
                ? configuration.GetSection("Email:JshProd")
                : configuration.GetSection("Email:JshDev"));
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(
                    configuration.GetConnectionString("JshStage"),
                    new MariaDbServerVersion(new Version(10, 3)));
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
