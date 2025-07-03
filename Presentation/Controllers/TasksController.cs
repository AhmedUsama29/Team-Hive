using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DataTransferObjects.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<ActionResult<TaskDetailedResponse>> CreateTask(TaskCreationDto taskCreationDto)
        {
            var task = await _serviceManager.TaskService.CreateTaskAsync(taskCreationDto); //send team id with auth

            return Ok(task);
        }
        //Auth for team members and users and supervisor and teacher
        [HttpPut("Edit")]
        public async Task<ActionResult<TaskDetailedResponse>> UpdateTask(TaskUpdateDto taskUpdateDto)
        {
            var UpdatedTask = await _serviceManager.TaskService.UpdateTaskAsync(taskUpdateDto);

            return Ok(UpdatedTask);
        }

        [HttpDelete("Delete")]
        public async Task DeleteTask(string Id)
        {
            await _serviceManager.TaskService.DeleteTaskAsync(Id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponse>>> GetAllTasks()
        {
            return Ok(await _serviceManager.TaskService.GetAllTasksAsync());
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<TaskDetailedResponse>> GetTaskById(string Id)
        {
            return Ok(await _serviceManager.TaskService.GetTaskByIdAsync(Id));
        }
    }
}
