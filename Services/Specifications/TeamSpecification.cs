using Domain.Models.Teams;
using Shared.DataTransferObjects.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class TeamSpecification : BaseSpecification<Team>
    {
        public TeamSpecification() : base(t => t.IsDeleted == false)  // Get All 
        {
            
        }

        public TeamSpecification(string Id) : base(t => (t.Id == Id) && (t.IsDeleted == false)) // get by id
        {

        }
    }
}
