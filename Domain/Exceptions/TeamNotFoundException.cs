using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class TeamNotFoundException(string teamId) : NotFoundException($"Team '{teamId}' is not found.")
    {
    }
}
