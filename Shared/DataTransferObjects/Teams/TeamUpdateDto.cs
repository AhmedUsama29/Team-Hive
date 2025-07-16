using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Teams
{
    public class TeamUpdateDto
    {
        public string Title { get; set; }
        public int MaxCapacity { get; set; }
        public bool IsLocked { get; set; }
        public string? Description { get; set; }

    }
}
