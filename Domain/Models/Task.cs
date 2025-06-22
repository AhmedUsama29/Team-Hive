using Domain.Models.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Task
    {

        public TeamMember AssignedTo { get; set; }
        public string? AssignedToId { get; set; }

    }
}
