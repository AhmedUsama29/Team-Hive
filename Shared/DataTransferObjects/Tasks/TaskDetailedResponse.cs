using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Tasks
{
    public class TaskDetailedResponse
    {

        public string Id { get; set; } = default!;

        public string Title { get; set; } = default!;

        public DateTime? DueDate { get; set; }
        public DateTime LastUpdatedAt { get; set; }
        public int AssignedById { get; set; } = default!;
        public int? CompletedById { get; set; }

        public DateTime CreatedOn { get; set; } = default!;

        public string? Description { get; set; }

        public int? AssignedToId { get; set; }

        public string? Priority { get; set; }
        public string Status { get; set; } = default!;

    }
}
