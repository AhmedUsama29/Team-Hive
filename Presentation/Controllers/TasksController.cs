using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DataTransferObjects.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/teams/{teamId}/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("Create")]
        public async Task<ActionResult<TaskDetailedResponse>> CreateTask(TaskCreationDto taskCreationDto ,[FromRoute] string teamId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var task = await _serviceManager.TaskService.CreateTaskAsync(taskCreationDto,teamId,userId!);

            return Ok(task);
        }

        [HttpPut("{taskId}/Edit")]
        public async Task<ActionResult<TaskDetailedResponse>> UpdateTask(TaskUpdateDto taskUpdateDto, [FromRoute] string teamId,[FromRoute] string taskId)
        {
            var UpdatedTask = await _serviceManager.TaskService.UpdateTaskAsync(taskId,taskUpdateDto);

            return Ok(UpdatedTask);
        }

        [HttpDelete("{taskId}/Delete")]
        public async Task DeleteTask([FromRoute] string teamId,[FromRoute] string taskId)
        {
            await _serviceManager.TaskService.DeleteTaskAsync(taskId);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskResponse>>> GetAllTasks([FromRoute]TaskQueryParameters taskQueryParameters,[FromRoute] string teamId)
        {
            taskQueryParameters.TeamId = teamId;
            return Ok(await _serviceManager.TaskService.GetAllTasksAsync(taskQueryParameters));
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<TaskDetailedResponse>> GetTaskById([FromRoute]string teamId,[FromRoute]string taskId)
        {
            return Ok(await _serviceManager.TaskService.GetTaskByIdAsync(taskId));
        }
    }
}
