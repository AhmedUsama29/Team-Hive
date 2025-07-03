using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Enums
{
    public enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Blocked,
        Cancelled
    }

    public enum Priority
    {
        Low,
        Medium,
        High,
        Critical
    }

}
