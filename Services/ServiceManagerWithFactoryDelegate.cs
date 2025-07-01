using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServiceManagerWithFactoryDelegate(Func<ITaskService> TaskFactory) : IServiceManager
    {
        public ITaskService TaskService => TaskFactory.Invoke();
    }
}
