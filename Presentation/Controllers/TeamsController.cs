using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DataTransferObjects.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TeamsController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<ActionResult<TeamResponse>> CreateTeam(TeamCreationDto teamCreationDto)
        {
            var mail = User.FindFirstValue(ClaimTypes.Email);

            return Ok(await _serviceManager.TeamService.CreateTeamAsync(teamCreationDto, mail));
        }

    }
}
