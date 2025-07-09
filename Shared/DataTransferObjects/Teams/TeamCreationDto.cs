using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Teams
{
    public class TeamCreationDto
    {
        public string Title { get; set; } = default!;
        public int MaxCapacity { get; set; }
        public string? Description { get; set; }
        public int SupervisorId { get; set; }
        public bool IsLocked { get; set; } = default!;

    }
}
