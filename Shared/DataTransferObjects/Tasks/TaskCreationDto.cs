using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Tasks
{
    public class TaskCreationDto
    {
        public string Title { get; set; } = default!;
        public DateTime? DueDate { get; set; }

        public string? Description { get; set; }

        public int? AssignedToId { get; set; }

        public string? Priority { get; set; }

    }
}
