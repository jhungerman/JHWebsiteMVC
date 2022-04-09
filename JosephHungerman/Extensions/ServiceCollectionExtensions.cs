﻿using System.Diagnostics.CodeAnalysis;
using JosephHungerman.Core.Options;
using JosephHungerman.Data;
using JosephHungerman.Data.Repositories;
using JosephHungerman.Helpers;
using JosephHungerman.Services;
using Microsoft.EntityFrameworkCore;
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
                    ? configuration.GetSection("Email:JshProd:ApiKey").ToString()
                    : configuration.GetSection("Email:JshDev:ApiKey").ToString();
            });
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
