using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects.TeamMembers
{
    public class TeamMemberResponse
    {
        public string Id { get; set; }
        public string Notes { get; set; }
        public string UserId { get; set; }
        public string Title { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
