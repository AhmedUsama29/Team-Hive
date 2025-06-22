using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Teams
{
    public class Team : BaseEntity<string>
    {

        public ICollection<TeamMember>? Members { get; set; }

    }
}
