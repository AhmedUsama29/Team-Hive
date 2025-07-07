using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using TaskHive.Factories;

namespace TaskHive
{
    public static class Extentions
    {

        public static IServiceCollection AddWebApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers();
            services.AddSwaggerServices();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationResponse;
            });


            return services;
        }

        private static void AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public static async Task<WebApplication> InitializeDbAsync(this WebApplication app)
        {

            using var Scope = app.Services.CreateScope(); //BG Services
            var dbInitializer = Scope.ServiceProvider.GetRequiredService<IDbInitializer>();
            await dbInitializer.InitializeIdentityAsync();
            return app;

        }

    }
}
