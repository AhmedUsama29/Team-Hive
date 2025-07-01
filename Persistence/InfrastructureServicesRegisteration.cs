using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data;
using Persistence.Identity;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class InfrastructureServicesRegisteration
    {

        public static IServiceCollection AddInfrastructureRegistration(this IServiceCollection services, IConfiguration configurations)
        {

            services.AddDbContext<TaskHiveDbContext>(options =>
                            options.UseSqlServer(configurations.GetConnectionString("TaskHiveConnection")));

            services.AddDbContext<TaskHiveIdentityDbContext>(options =>
                            options.UseSqlServer(configurations.GetConnectionString("IdentityTaskHiveConnection")));

            services.AddScoped<IUnitOfWork,UnitOfWork>();

            services.RegisterIdentity();

            return services;
        }

        private static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {

            //services.AddIdentity<ApplicationUser, IdentityRole>();

            services.AddIdentityCore<ApplicationUser>(config =>
            {
                config.User.RequireUniqueEmail = true;
            })
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<TaskHiveIdentityDbContext>();

            return services;
        }

    }
}
