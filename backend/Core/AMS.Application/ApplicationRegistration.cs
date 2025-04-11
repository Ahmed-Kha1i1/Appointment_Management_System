using AMS.Application.Common.Validators;
using AMS.Application.Common.Exceptions_Handlers;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;
using AMS.Application.Features.Appointments;
using AMS.Doman.Entities;
using Microsoft.AspNetCore.Identity;
using AMS.Application.Contracts.Persistence.Base;
using AMS.Application.Common.Models;

namespace AMS.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExceptionHandler<GlobalExceptionHandler>();

            //Confiure Auto Mapper 
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Configure Mediator
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            //Configure AppointmentSettings
            services.Configure<AppointmentSettings>(configuration.GetSection("AppointmentSettings"));
            services.Configure<RecaptchaSettings>(configuration.GetSection("RecaptchaSettings"));

            // Configure Fluent Validation
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies(), ServiceLifetime.Scoped);
            services.AddFluentValidationAutoValidation(config =>
            {
                config.DisableBuiltInModelValidation = true;
                config.EnableBodyBindingSourceAutomaticValidation = true;
                config.EnableFormBindingSourceAutomaticValidation = true;
                config.EnableQueryBindingSourceAutomaticValidation = true;
                config.EnablePathBindingSourceAutomaticValidation = true;
                config.OverrideDefaultResultFactoryWith<ValidationResultFactory>();
            });

            services.AddHttpContextAccessor();

            return services;
        }
    }
}
