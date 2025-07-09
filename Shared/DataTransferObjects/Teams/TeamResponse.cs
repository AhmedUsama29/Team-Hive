using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.Teams
{
    public class TeamResponse
    {
        public string Title { get; set; }
        public string JoinCode { get; set; }
        public int MaxCapacity { get; set; }
        public bool IsLocked { get; set; }

    }
}
