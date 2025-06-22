using Microsoft.EntityFrameworkCore;
using Persistence.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data
{
    public class TaskHiveDbContext(DbContextOptions<TaskHiveDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskHiveDbContext).Assembly);

        }

        //Dbset
    }
}
