using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.MappingProfiles;
using ServicesAbstraction;
using Shared.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ApplicationServiceRegisteration
    {

        public static IServiceCollection AddAplicationServices(this IServiceCollection services,
                                                                IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(TaskProfile).Assembly);

            services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();

            services.AddScoped<ITaskService, TaskService>();

            services.AddScoped<IAuthenticationService, AuthenticationService>();

            services.AddScoped<ITeamService, TeamService>();

            services.AddScoped<Func<ITaskService>>(provider => ()
                => provider.GetRequiredService<ITaskService>());
            
            services.AddScoped<Func<IAuthenticationService>>(provider => ()
                => provider.GetRequiredService<IAuthenticationService>());

            services.AddScoped<Func<ITeamService>>(provider => ()
                => provider.GetRequiredService<ITeamService>());

            services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));

            return services;
        }
    }
}
