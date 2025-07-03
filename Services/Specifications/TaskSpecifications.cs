using Domain.Models;
using Shared.DataTransferObjects.Tasks;
using Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Task = Domain.Models.Task;
using TaskStatus = Domain.Models.TaskStatus;


namespace Services.Specifications
{
    public class TaskSpecifications : BaseSpecification<Task>
    {
        public TaskSpecifications(TaskQueryParameters taskQueryParameters) : base(CreateCriteria(taskQueryParameters))  // Get All 
        {
            AddInclude(t => t.CompletedBy);
            AddInclude(t => t.AssignedBy);
            AddInclude(t => t.AssignedTo);
        }

        public TaskSpecifications(string Id) : base(t => t.Id == Id) // get by id
        {
            AddInclude(t => t.CompletedBy);
            AddInclude(t => t.AssignedBy);
            AddInclude(t => t.AssignedTo);
        }

        private static Expression<Func<Task, bool>> CreateCriteria(TaskQueryParameters taskQueryParameters)
        {
            var mappedPriority = MapPriority(taskQueryParameters.Priority);
            var mappedStatus = MapStatus(taskQueryParameters.Status);

            return t =>
                !t.IsDeleted &&
                (!mappedPriority.HasValue || t.Priority == mappedPriority.Value) &&
                (!mappedStatus.HasValue || t.Status == mappedStatus.Value);
        }



        #region Mapping Enums

        private static Domain.Models.Priority? MapPriority(Shared.Enums.Priority? priority)
        {
            return priority.HasValue
                ? (Domain.Models.Priority?)Enum.Parse(typeof(Domain.Models.Priority), priority.Value.ToString())
                : null;
        }

        private static Domain.Models.TaskStatus? MapStatus(Shared.Enums.TaskStatus? status)
        {
            return status.HasValue
                ? (Domain.Models.TaskStatus?)Enum.Parse(typeof(Domain.Models.TaskStatus), status.Value.ToString())
                : null;
        }


        #endregion
    }
}
