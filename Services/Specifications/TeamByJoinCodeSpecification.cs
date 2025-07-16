using Domain.Models.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class TeamByJoinCodeSpecification : BaseSpecification<Team>
    {
        public TeamByJoinCodeSpecification(string code) : base((t => (t.JoinCode == code) && !t.IsDeleted))
        {
        }
    }
}
