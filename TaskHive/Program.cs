using Persistence;

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

            var app = builder.Build();

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
