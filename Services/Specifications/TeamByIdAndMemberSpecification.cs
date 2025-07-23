using Domain.Models.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class TeamByIdAndMemberSpecification : BaseSpecification<Team>
    {
        public TeamByIdAndMemberSpecification(string teamId, string userId)
        : base(t => t.Id == teamId && t.IsDeleted == false && t.Members.Any(m => m.UserId == userId))
        {
            AddInclude(t => t.Members);
        }

    }
}
