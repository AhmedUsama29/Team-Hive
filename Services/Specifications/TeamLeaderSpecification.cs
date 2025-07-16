using Domain.Models.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class TeamLeaderSpecification : BaseSpecification<Team>
    {
        public TeamLeaderSpecification(string teamId, string userId): base(t =>
                t.Id == teamId &&
                t.Members.Any(m => m.UserId == userId && m.IsLeader)
            )
        {
            AddInclude(t => t.Members);
        }
    }
}
