using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class TaskNotFoundException(string id) : NotFoundException($"Task with this id : {id} is Not Found")
    {
    }
}
