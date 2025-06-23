using Domain.Models.Teams;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class TeamConfigurations : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {

            builder.Property(t => t.Description)
                     .HasMaxLength(500)
                     .IsRequired(false);

            builder.Property(t => t.LeaderId)
                   .IsRequired();

            builder.Property(t => t.MaxCapacity)
                   .IsRequired();

            builder.HasMany(t => t.Issues)
                   .WithOne(i => i.Team)
                   .HasForeignKey(i => i.TeamId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
