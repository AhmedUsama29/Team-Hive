using Domain.Models.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class TeamMembersByTeamIdSpecification : BaseSpecification<TeamMember>
    {


        public TeamMembersByTeamIdSpecification() : base(t => t.IsDeleted == false)
        {
            AddInclude(t => t.Team);
        }

        public TeamMembersByTeamIdSpecification(string teamId) : base(t => (t.IsDeleted == false) && (t.TeamId == teamId))
        {
            AddInclude(t => t.Team);
        }
    }
}
