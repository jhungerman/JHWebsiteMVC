using JosephHungerman.Identity.Models;
using JosephHungerman.Identity.Repositories;
using JosephHungerman.Identity.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace JosephHungerman.Identity.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<IdentityContext>(options =>
            {
                if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                {
                    options.UseMySql(
                        configuration.GetConnectionString("JshIdStage"),
                        new MariaDbServerVersion(new Version(10, 3)));
                }
                else
                {
                    options.UseMySql(
                        configuration.GetConnectionString("JshIdProd"),
                        new MariaDbServerVersion(new Version(10, 5)));
                }
            });
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<IdentityContext>()
                .AddDefaultUI()
                .AddUserValidator<ApplicationUserValidator>()
                .AddDefaultTokenProviders()
                .AddPwnedPasswordValidator<ApplicationUser>(options =>
                {
                    options.ErrorMessage =
                        "This password has been flagged by haveibeenpwned.com as being part of a data breach. Try another password.";
                });

            services.AddAuthentication();
            services.AddPwnedPasswordHttpClient(minimumFrequencyToConsiderPwned: 100)
                .AddTransientHttpErrorPolicy(p => p.RetryAsync(retryCount: 3))
                .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(1)));

            services.AddScoped<IEmailSender, ApplicationEmailSender>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
