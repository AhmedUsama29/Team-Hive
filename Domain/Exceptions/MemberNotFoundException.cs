using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public class MemberNotFoundException() : NotFoundException("This member is not found")
    {
        
    }
}
