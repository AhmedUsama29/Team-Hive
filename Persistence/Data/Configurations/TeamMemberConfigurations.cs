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
    public class TeamMemberConfigurations : IEntityTypeConfiguration<TeamMember>
    {
        public void Configure(EntityTypeBuilder<TeamMember> builder)
        {

            builder.Property(x => x.Title)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.Property(x => x.IsLeader)
                 .IsRequired()
                 .HasDefaultValue(false);

            builder.Property(x => x.Notes)
                 .HasMaxLength(500)
                 .IsRequired(false);

            builder.HasOne(x => x.Team)
                   .WithMany(x => x.Members)
                   .HasForeignKey(x => x.TeamId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Tasks)
                   .WithOne(x => x.AssignedTo)
                   .HasForeignKey(x => x.AssignedToId)
                   .OnDelete(DeleteBehavior.SetNull);
            
        }
    }
}
