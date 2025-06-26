using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Tasks
{
    public class TaskResponse
    {

        public string Id { get; set; }
        public string Title { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public int AssignedToId { get; set; }
        public DateTime? DueDate { get; set; }

    }
}
