using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Domain.Models.Task;

namespace Persistence.Data.Configurations
{
    public class TaskConfigurations : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {

            builder.Property(t => t.Status)
                .HasConversion<string>()
                .HasMaxLength(10);

            builder.Property(t => t.Priority)
                .HasConversion<string>()
                .HasMaxLength(8);

            builder.Property(t => t.Description)
                     .HasMaxLength(500)
                     .IsRequired(false);

            builder.HasOne(t => t.AssignedBy)
                   .WithMany()
                   .HasForeignKey(t => t.AssignedById)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(t => t.CompletedBy)
                     .WithMany()
                     .HasForeignKey(t => t.CompletedById)
                     .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
