using AMS.API.DTO;
using AMS.Application;
using AMS.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace AMS.API
{
    public class Startup
    {
        private string AllowsOrigins = "AllowSpicificOrigin";
        private readonly IConfigurationRoot _configuration;

        public Startup(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureBuilder(WebApplicationBuilder builder)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configure CORS from settings
            var corsSettings = _configuration.GetSection("CorsSettings").Get<CorsSettings>();

            services.AddAuthorization();

            // Add services to the container.
            services.AddApplicationServices(_configuration)
                    .AddPersistenceServices(_configuration);


            services.AddSwaggerGen();
            services.AddProblemDetails();
            services.AddControllers(options =>
            {
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
            });


            services.AddCors(options =>
            {
                options.AddPolicy(AllowsOrigins, policy =>
                {
                    policy.WithOrigins(corsSettings?.AllowedOrigins ?? Array.Empty<string>())
                          .AllowCredentials()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

        }
        
        public void Configure(WebApplication app)
        {
           
            app.UseSwagger();
            app.UseSwaggerUI();
            
            app.UseExceptionHandler();
            app.UseHttpsRedirection();
            app.UseCors(AllowsOrigins);

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
        }
        
    }
}