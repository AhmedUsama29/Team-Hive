using Persistence;
using Services;
using TaskHive.Middelwares;

namespace TaskHive
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddInfrastructureRegistration(builder.Configuration);
            builder.Services.AddWebApplicationServices(builder.Configuration);
            builder.Services.AddAplicationServices(builder.Configuration);

            var app = builder.Build();

            app.UseMiddleware<CustomExceptionHandlerMiddleware>();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.DocumentTitle = "Task Hive App";
                    options.EnableFilter();
                    options.DisplayRequestDuration();
                });
            }

            app.UseHttpsRedirection();

            app.MapControllers();

            app.Run();
        }
    }
}
