using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Teams
{
    public class Team : BaseEntity<string>
    {

        public int MaxCapacity { get; set; } = default!;
        public string JoinCode { get; set; } = null!;
        public string? Description { get; set; }
        public int SupervisorId { get; set; } = default!;
        public bool IsLocked { get; set; } = default!;
        public int LeaderId { get; set; } = default!;
        public ICollection<TeamMember> Members { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<Issue> Issues { get; set; }

    }
}
