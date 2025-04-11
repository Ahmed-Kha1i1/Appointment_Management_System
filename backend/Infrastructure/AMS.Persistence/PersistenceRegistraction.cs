using AMS.Application.Contracts;
using AMS.Application.Contracts.Persistence;
using AMS.Application.Contracts.Persistence.Base;
using AMS.Application.Features.Appointments;
using AMS.Persistence;
using AMS.Persistence.Options;
using AMS.Persistence.Repositories;
using AMS.Persistence.Repositories.Base;
using AMS.Persistence.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace AMS.Persistence
{
    public static class PersistenceRegistraction
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IAppointmentRepository,AppointmentRepository>();
            services.AddScoped<IDoctorRepository,DoctorRepository>();
            services.AddScoped<IPatientRepository,PatientRepository>();
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IUserRoleRepository,UserRoleRepository>();
            services.AddScoped<IRoleRepository,RoleRepository>();
            services.AddScoped<ISpecializationRepository,SpecializationRepository>();
            services.AddScoped<IRefreshTokenRepository,RefreshTokenRepository>();
            services.AddScoped<IAuthRespository,AuthRespository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Configure Jwt Settings
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            var JwtOptions = configuration.GetSection("JwtSettings").Get<JwtSettings>()!;

            services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));

            //Configure Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = JwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = JwtOptions.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SigningKey)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            //register DbContext
            services.AddDbContext<AppDbContext>();
            return services;
        }
    }
}
