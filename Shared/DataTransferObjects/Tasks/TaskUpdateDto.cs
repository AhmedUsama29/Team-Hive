using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Tasks
{
    public class TaskUpdateDto
    {
        //public string Id { get; set; }
        public string Title { get; set; } = default!;
        public string Status { get; set; } = default!;
        public DateTime? DueDate { get; set; }

        public string? Description { get; set; }

        public string? Priority { get; set; }

        public int? CompletedById { get; set; }
        public int? AssignedToId { get; set; }

    }
}
