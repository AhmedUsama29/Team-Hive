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

        public Task<TeamResponse> CreateTeamAsync(TeamCreationDto teamCreationDto, string userId);
        public Task<TeamResponse> UpdateTeamSettingsAsync(string teamId,string userId, TeamUpdateDto teamUpdateDto);
        public Task<TeamResponse> GetTeamByIdAsync(string teamId);
        public Task<bool> DeleteTeamAsync(string teamId, string userId);
        public Task<IEnumerable<TeamResponse>> GetAllTeamsByUserIdAsync(string userId);

        public Task<bool> JoinTeam(string joinCode, string userId);

    }
}
