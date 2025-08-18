using Task = Domain.Models.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Services.Specifications
{
    public class TaskbyTeamIdSpecification : BaseSpecification<Task>
    {
        public TaskbyTeamIdSpecification(string teamId) : base(t => (t.TeamId == teamId) && (t.IsDeleted == false))
        {
            AddInclude(t => t.AssignedBy);
            AddInclude(t => t.Team);
            AddInclude(t => t.AssignedTo);
            AddInclude(t => t.CompletedBy);
        }
    }
}
