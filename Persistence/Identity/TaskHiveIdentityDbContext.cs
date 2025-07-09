using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Identity
{
    public class TaskHiveIdentityDbContext(DbContextOptions<TaskHiveIdentityDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");

            builder.Entity<ApplicationUser>().Property(T => T.FirstName)
                .IsRequired()
                .HasColumnType("nvarchar(15)");

            builder.Entity<ApplicationUser>().Property(T => T.LastName)
                .IsRequired()
                .HasColumnType("nvarchar(15)");

            builder.Entity<ApplicationUser>().Property(T => T.DateOfBirth)
                .IsRequired()
                .HasColumnType("date");

            builder.Entity<ApplicationUser>().Property(T => T.UserType)
                .HasConversion<string>()
                .IsRequired()
                .HasColumnType("varchar(7)");

            builder.Entity<ApplicationUser>().Property(T => T.Gender)
                .HasConversion<string>()
                .IsRequired()
                .HasColumnType("varchar(6)");
        }

    }
}
