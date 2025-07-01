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
        public async Task<ActionResult<Task<TaskDetailedResponse>>> CreateTask(TaskCreationDto taskCreationDto)
        {
            var task = await _serviceManager.TaskService.CreateTaskAsync(taskCreationDto); //send team id with auth

            return Ok(task);
        }

    }
}
