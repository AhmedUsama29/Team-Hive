using Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Teams
{
    public class TeamMember : BaseEntity<int>
    {
        public bool IsLeader { get; set; }
        public string Notes { get; set; }
        public string UserId { get; set; }
        public Team Team { get; set; }
        public string TeamId { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
