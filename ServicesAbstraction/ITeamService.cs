using Shared.DataTransferObjects.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface ITeamService
    {

        public Task<TeamResponse> CreateTeamAsync(TeamCreationDto teamCreationDto, string mail);
        public Task<TeamResponse> UpdateTeamSettingsAsync(TeamUpdateDto teamUpdateDto);
        public Task<TeamResponse> GetTeamByIdAsync(string teamId); // still
        public Task<bool> DeleteTeamAsync(string teamId);
        public Task<IEnumerable<TeamResponse>> GetAllTeamsByUserIdAsync(string userId);

    }
}
