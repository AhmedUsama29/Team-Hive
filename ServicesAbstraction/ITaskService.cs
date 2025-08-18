using Shared.DataTransferObjects.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface ITaskService
    {   
        Task<IEnumerable<TaskResponse>> GetAllTasksAsync(TaskQueryParameters taskQueryParameters);
        Task<TaskDetailedResponse> GetTaskByIdAsync(string id);
        Task<TaskDetailedResponse> CreateTaskAsync(TaskCreationDto taskCreationDto,string teamId,string userId);
        Task<TaskDetailedResponse> UpdateTaskAsync(TaskUpdateDto taskUpdateDto);
        Task DeleteTaskAsync(string id);
    }
}
