using Domain.Models.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class TeamMemberByUserIdAndTeamIdSpecification : BaseSpecification<TeamMember>
    {

        public TeamMemberByUserIdAndTeamIdSpecification(string teamId,string UserId) : base(t => (!t.IsDeleted)
                                                                                            && (t.UserId == UserId)
                                                                                            && (t.TeamId == teamId))
        {
            AddInclude(t => t.Team);
            AddInclude(t => t.Tasks);

        }
        

    }
}
