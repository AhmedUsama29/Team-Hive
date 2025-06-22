using Domain.Models;
using Domain.Models.Teams;
using Microsoft.EntityFrameworkCore;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Domain.Models.Task;

namespace Persistence.Data
{
    public class TaskHiveDbContext(DbContextOptions<TaskHiveDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskHiveDbContext).Assembly);

        }

        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Issue> Issues { get; set; }
    }
}
