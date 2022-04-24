using System.Diagnostics.CodeAnalysis;
using JosephHungerman.Core.Options;
using JosephHungerman.Helpers;
using JosephHungerman.Services;
using JosephHungerman.Services.Interfaces;
using SendGrid.Extensions.DependencyInjection;

namespace JosephHungerman.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production"
                ? configuration.GetSection("Email:JshProd")
                : configuration.GetSection("Email:JshDev"));
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.Configure<CaptchaSettings>(configuration.GetSection("Captcha"));
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            services.AddSendGrid(options =>
            {
                options.ApiKey = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production"
                    ? configuration.GetSection("Email:JshProd:ApiKey").Value
                    : configuration.GetSection("Email:JshDev:ApiKey").Value;
            });
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICaptchaService, CaptchaService>();
            services.AddScoped<IResumeService, ResumeService>();
            services.AddScoped<IQuoteService, QuoteService>();
            services.AddScoped<IAboutService, AboutService>();

            return services;
        }
    }
}
