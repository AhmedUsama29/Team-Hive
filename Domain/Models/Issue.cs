using Domain.Models.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{

    public enum IssueStatus
    {
        Open,
        InReview,
        Resolved,
        Rejected
    }
    public class Issue : BaseEntity<string>
    {

        public DateTime? ResolvedAt { get; set; }
        public string? Description { get; set; }
        public IssueStatus Status { get; set; }
        public Priority Priority { get; set; }

        public TeamMember CreatedBy { get; set; }

        public int CreatedById { get; set; }

        public Team Team { get; set; }
        public string TeamId { get; set; }

    }
}
