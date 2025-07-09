using Domain.Models.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class TeamMemberSpecifiaction : BaseSpecification<TeamMember>
    {
        public TeamMemberSpecifiaction() : base(t => t.IsDeleted == false)  // Get All 
        {

        }

        public TeamMemberSpecifiaction(int Id) : base(t => (t.Id == Id) && (t.IsDeleted == false)) // get by id
        {

        }
    }
}
