using Domain.Models.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Blocked,
        Cancelled
    }

    public enum Priority
    {
        Low,
        Medium,
        High,
        Critical
    }

    public class Task : BaseEntity<string>
    {

        public TaskStatus Status { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        public DateTime? DueDate { get; set; }

        public string? Description { get; set; }

        public Priority Priority { get; set; }

        public TeamMember? CompletedBy { get; set; }

        public int? CompletedById { get; set; }

        public TeamMember AssignedBy { get; set; } = null!;
        public int AssignedById { get; set; } = default!;

        public TeamMember? AssignedTo { get; set; }
        public int? AssignedToId { get; set; }

        public Team Team { get; set; }
        public string TeamId { get; set; }

    }
}
