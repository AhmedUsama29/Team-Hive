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
        Task<IEnumerable<TaskResponse>> GetAllTasksAsync();
        Task<TaskDetailedResponse> GetTaskByIdAsync(string id);
        Task<TaskCreationDto> CreateTaskAsync(TaskCreationDto taskCreationDto);
        Task<TaskUpdateDto> UpdateTaskAsync(TaskUpdateDto taskUpdateDto);
        Task DeleteTaskAsync(string id);
    }
}
