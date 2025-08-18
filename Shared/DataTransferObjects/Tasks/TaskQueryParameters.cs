using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskStatus = Shared.Enums.TaskStatus;

namespace Shared.DataTransferObjects.Tasks
{
    public class TaskQueryParameters
    {
        public string TeamId { get; set; } = default!;
        public TaskStatus? Status { get; set; }

        public Priority? Priority { get; set; }


    }
}
