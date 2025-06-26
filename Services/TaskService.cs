using AutoMapper;
using Domain.Contracts;
using ServicesAbstraction;
using Shared.DataTransferObjects.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TaskService(IUnitOfWork _unitOfWork, IMapper _mapper) : ITaskService
    {
        public Task<TaskCreationDto> CreateTaskAsync(TaskCreationDto taskCreationDto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteTaskAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TaskResponse>> GetAllTasksAsync()
        {
            var TasksRepo = _unitOfWork.GetRepository<Domain.Models.Task, string>();

            var tasks = await TasksRepo.GetAllAsync();

            return _mapper.Map<IEnumerable<TaskResponse>>(tasks);

        }

        public Task<TaskDetailedResponse> GetTaskByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<TaskUpdateDto> UpdateTaskAsync(TaskUpdateDto taskUpdateDto)
        {
            throw new NotImplementedException();
        }

    }
}
