using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class IssueConfigurations : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.Property(t => t.Description)
                   .HasMaxLength(500)
                   .IsRequired(false);

            builder.Property(t => t.Status)
                .HasConversion<string>()
                .HasMaxLength(8);

            builder.Property(t => t.Priority)
                   .HasConversion<string>()
                   .HasMaxLength(8);

            builder.HasOne(i => i.CreatedBy)
                   .WithMany()
                   .HasForeignKey(i => i.CreatedById)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(i => i.Team)
                   .WithMany(t => t.Issues)
                   .HasForeignKey(i => i.TeamId)
                   .OnDelete(DeleteBehavior.SetNull);

        }
    }
}
