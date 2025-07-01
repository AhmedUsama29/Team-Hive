using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
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
        public async Task<TaskDetailedResponse> CreateTaskAsync(TaskCreationDto taskCreationDto)
        {
            var repo = _unitOfWork.GetRepository<Domain.Models.Task, string>();

            var task = _mapper.Map<Domain.Models.Task>(taskCreationDto);

            repo.Add(task);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TaskDetailedResponse>(task);
        }

        public async Task DeleteTaskAsync(string id)
        {
            var repo = _unitOfWork.GetRepository<Domain.Models.Task, string>();

            var task = await repo.GetByIdAsync(id);

            if (task == null)
                throw new TaskNotFoundException(id);

            repo.Delete(task);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaskResponse>> GetAllTasksAsync()
        {
            var TasksRepo = _unitOfWork.GetRepository<Domain.Models.Task, string>();

            var tasks = await TasksRepo.GetAllAsync();

            return _mapper.Map<IEnumerable<TaskResponse>>(tasks);

        }

        public async Task<TaskDetailedResponse> GetTaskByIdAsync(string id)
        {
            var repo = _unitOfWork.GetRepository<Domain.Models.Task, string>();

            var task = await repo.GetByIdAsync(id);

            if (task == null)
                throw new TaskNotFoundException(id);

            return _mapper.Map<TaskDetailedResponse>(task);
        }

        public async Task<TaskDetailedResponse> UpdateTaskAsync(TaskUpdateDto taskUpdateDto)
        {
            var repo = _unitOfWork.GetRepository<Domain.Models.Task, string>();

            var task = _mapper.Map<Domain.Models.Task>(taskUpdateDto);

            repo.Update(task);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TaskDetailedResponse>(task);

        }

    }
}
