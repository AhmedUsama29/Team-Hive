using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Identity;
using Domain.Models.Teams;
using Microsoft.AspNetCore.Identity;
using Services.Specifications;
using ServicesAbstraction;
using Shared.DataTransferObjects.Tasks;
using Shared.DataTransferObjects.TeamMembers;
using Shared.DataTransferObjects.Teams;
using Task = Domain.Models.Task;
namespace Services
{
    public class TeamService(IUnitOfWork _unitOfWork,
                             IMapper _mapper,
                             UserManager<ApplicationUser> _userManager) : ITeamService
    {
        public async Task<TeamResponse> CreateTeamAsync(TeamCreationDto dto, string userId)
        {

            if (string.IsNullOrWhiteSpace(userId))
                throw new UserNotFoundException(userId);

            var teamRepo = _unitOfWork.GetRepository<Team, string>();
            var memberRepo = _unitOfWork.GetRepository<TeamMember, int>();

            var team = _mapper.Map<Team>(dto);
            team.Id = Guid.NewGuid().ToString();
            team.CreatedOn = DateTime.UtcNow;
            string code;
            do
            {
                code = GenerateRandomCode();
            } while ((await teamRepo.GetAllAsync(new TeamByJoinCodeSpecification(code))).Any());

            team.JoinCode = code;
            team.MaxCapacity = dto.MaxCapacity == 0 ? 50 : dto.MaxCapacity;

            var user = await _userManager.FindByIdAsync(userId);

            var leader = new TeamMember
            {
                UserId = userId,
                TeamId = team.Id,
                CreatedOn = DateTime.UtcNow,
                IsLeader = true,
                Title = $"{user!.FirstName} {user.LastName}",
            };
            team.Members = new List<TeamMember> { leader };

            teamRepo.Add(team);

            await _unitOfWork.SaveChangesAsync();

            team.LeaderId = leader.Id;
            teamRepo.Update(team);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TeamResponse>(team);
        }

        public async Task<bool> DeleteTeamAsync(string teamId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new UserNotFoundException(userId);

            var repo = _unitOfWork.GetRepository<Team, string>();

            var team = await repo
                .GetByIdAsync(new TeamLeaderSpecification(teamId, userId))
                ?? throw new UnauthorizedAccessException("You are not authorized to delete this team.");

            #region Delete Team Members
            var membersRepo = _unitOfWork.GetRepository<TeamMember, int>();

            var members = await membersRepo.GetAllAsync(new TeamMembersByTeamIdSpecification(teamId));
            if (members.Any())
            {
                foreach (var member in members)
                {
                    member.IsDeleted = true;
                    membersRepo.Update(member);
                }
            }
            #endregion

            #region Delete Team Tasks

            var tasksRepo = _unitOfWork.GetRepository<Task, string>();
            var tasks = await tasksRepo.GetAllAsync(new TaskbyTeamIdSpecification(teamId));
            if (tasks.Any())
            {
                foreach (var task in tasks)
                {
                    task.IsDeleted = true;
                    tasksRepo.Update(task);
                }
            }

            #endregion

            team.IsDeleted = true;
            repo.Update(team);

            return await _unitOfWork.SaveChangesAsync() > 0;
        }


        public async Task<IEnumerable<TeamMemberResponse>> GetAllTeamMembersAsync(string teamId, string userId)
        {
            var teamRepo = _unitOfWork.GetRepository<Team, string>();
            var memberRepo = _unitOfWork.GetRepository<TeamMember, int>();

            var team = await teamRepo.GetByIdAsync(new TeamByIdAndMemberSpecification(teamId, userId));
            if (team == null)
                throw new UnauthorizedAccessException("You are not a member of this team.");

            var members = await memberRepo.GetAllAsync(new TeamMembersByTeamIdSpecification(teamId));

            return _mapper.Map<IEnumerable<TeamMemberResponse>>(members);

        }

        public async Task<IEnumerable<TeamResponse>> GetAllTeamsByUserIdAsync(string userId)
        {
            var teamRepo = _unitOfWork.GetRepository<Team, string>();
            var memnberRepo = _unitOfWork.GetRepository<TeamMember, int>();

            var allTeams = await teamRepo.GetAllAsync(new TeamSpecification());

            if (allTeams == null)
                return Enumerable.Empty<TeamResponse>();

            var filteredTeams = allTeams
                .Where(t => t.Members != null && t.Members.Any(m => m.UserId == userId));

            return _mapper.Map<IEnumerable<TeamResponse>>(filteredTeams);


        }

        public async Task<TeamMemberResponse> GetMemberAsync(int memberId,string userId)
        {
            var memberRepo = _unitOfWork.GetRepository<TeamMember, int>();
            var teamRepo = _unitOfWork.GetRepository<Team, string>();

            var member = await memberRepo.GetByIdAsync(new TeamMemberSpecification(memberId))
            ?? throw new MemberNotFoundException();

            var team = await teamRepo.GetByIdAsync(new TeamByIdAndMemberSpecification(member.TeamId, userId));
            if (team == null)
                throw new UnauthorizedAccessException("You are not authorized to view this member.");

            return _mapper.Map<TeamMemberResponse>(member);

        }

        public async Task<TeamResponse> GetTeamByIdAsync(string teamId, string userId)
        {
            var repo = _unitOfWork.GetRepository<Team, string>();

            var specs = new TeamByIdAndMemberSpecification(teamId,userId);

            var team = await repo.GetByIdAsync(specs)
                ?? throw new TeamNotFoundException(teamId);

            return _mapper.Map<TeamResponse>(team);
        }

        public async Task<bool> JoinTeamAsync(string joinCode, string userId)
        {
            var teamRepo = _unitOfWork.GetRepository<Team, string>();
            var memberRepo = _unitOfWork.GetRepository<TeamMember, int>();

            var team = (await teamRepo.GetByIdAsync(new TeamByJoinCodeSpecification(joinCode)))
                       ?? throw new TeamNotFoundException(joinCode);

            if (team.IsLocked)
                throw new Exception("Team is locked");

            if (team.Members.Count >= team.MaxCapacity)
                throw new Exception("Team is full");

            var alreadyMember = team.Members.Any(m => m.UserId == userId);
            if (alreadyMember)
                throw new Exception("User is already a member of this team");

            var user = await _userManager.FindByIdAsync(userId);
            var member = new TeamMember
            {
                TeamId = team.Id,
                UserId = userId,
                CreatedOn = DateTime.UtcNow,
                IsLeader = false,
                Title = $"{user!.FirstName} {user.LastName}"
            };

            memberRepo.Add(member);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> LeaveTeamAsync(string teamId, string userId)
        {
            var teamRepo = _unitOfWork.GetRepository<Team, string>();
            var memberRepo = _unitOfWork.GetRepository<TeamMember, int>();


            var team = await teamRepo.GetByIdAsync(new TeamSpecification(teamId))
                        ?? throw new TeamNotFoundException(teamId);

            var member = team.Members.FirstOrDefault(m => m.UserId == userId);
            if (member == null)
                throw new UnauthorizedAccessException("You are not a member of this team.");


            if (member.IsLeader && team.Members.Count > 1)
            {
                memberRepo.Delete(member);
                var newLeader = team.Members.FirstOrDefault(m => m.UserId != userId);
                if (newLeader is not null)
                {
                    newLeader.IsLeader = true;
                    newLeader.Title = "Leader";
                    team.LeaderId = newLeader.Id;
                }

            }
            else if (team.Members.Count == 1)
            {
                memberRepo.Delete(member);
                return await DeleteTeamAsync(teamId, userId);
                
            }
            else
            {
                memberRepo.Delete(member);
            }

            return await _unitOfWork.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveMemberAsync(int memberId,string teamId,string userId)
        {
            var teamRepo = _unitOfWork.GetRepository<Team, string>();
            var memberRepo = _unitOfWork.GetRepository<TeamMember, int>();

            var team = await teamRepo.GetByIdAsync(new TeamLeaderSpecification(teamId, userId))
                ?? throw new UnauthorizedAccessException("Only the team leader can remove members.");

            var member = team.Members.FirstOrDefault(m => m.Id == memberId)
                ?? throw new MemberNotFoundException();

            if (member.IsLeader)
                throw new UnauthorizedAccessException("You cannot remove a team leader.");

            memberRepo.Delete(member);
            return await _unitOfWork.SaveChangesAsync() > 0;

        }

        public async Task<TeamResponse> UpdateTeamSettingsAsync(string teamId,string userId, TeamUpdateDto dto)
        {
            if(string.IsNullOrWhiteSpace(userId))
                throw new UserNotFoundException(userId);

            var repo = _unitOfWork.GetRepository<Team, string>();

            var team = await repo
                .GetByIdAsync(new TeamLeaderSpecification(teamId, userId))
                ?? throw new UnauthorizedAccessException("You are not authorized to update this team.");

            _mapper.Map(dto, team);

            repo.Update(team);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TeamResponse>(team);
        }

        private string GenerateRandomCode(int length = 6)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}
