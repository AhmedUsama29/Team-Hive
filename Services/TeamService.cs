using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Identity;
using Domain.Models.Teams;
using Microsoft.AspNetCore.Identity;
using Services.Specifications;
using ServicesAbstraction;
using Shared.DataTransferObjects.Tasks;
using Shared.DataTransferObjects.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TeamService(IUnitOfWork _unitOfWork,
                             IMapper _mapper) : ITeamService
    {
        //public async Task<TeamResponse> CreateTeamAsync(TeamCreationDto teamCreationDto, string userId)
        //{
        //    var team = _mapper.Map<Team>(teamCreationDto)
        //        ?? throw new Exception("Can't Create This Team");

        //    var teamRepo = _unitOfWork.GetRepository<Team, string>();
        //    var memberRepo = _unitOfWork.GetRepository<TeamMember, int>();
        //    var user = await _userManager.FindByIdAsync(userId);

        //    team.Id = Guid.NewGuid().ToString();
        //    team.CreatedOn = DateTime.UtcNow;
        //    team.MaxCapacity = team.MaxCapacity == 0 ? 50 : teamCreationDto.MaxCapacity;

        //    string joinCode;
        //    do
        //    {
        //        joinCode = GenerateRandomCode();
        //    } while (await teamRepo.(new TeamByJoinCodeSpecification(joinCode)));   ///////////////////////////
        //    team.JoinCode = joinCode;

        //    teamRepo.Add(team);
        //    await _unitOfWork.SaveChangesAsync();

        //    var teamMember = new TeamMember
        //    {
        //        UserId = user!.Id,
        //        TeamId = team.Id,
        //        CreatedOn = DateTime.UtcNow,
        //        IsLeader = true,
        //        Title = user.FirstName
        //    };

        //    memberRepo.Add(teamMember);
        //    await _unitOfWork.SaveChangesAsync();

        //    var leaderId = teamMember.Id;
        //    team.LeaderId = leaderId;

        //    teamRepo.Update(team);
        //    await _unitOfWork.SaveChangesAsync();

        //    return _mapper.Map<TeamResponse>(team)
        //           ?? throw new Exception("Can't Map This Team to Response DTO");
        //}

        public async Task<TeamResponse> CreateTeamAsync(TeamCreationDto dto, string userId)
        {

            if (string.IsNullOrWhiteSpace(userId))
                throw new UserNotFoundException(userId);

            var teamRepo = _unitOfWork.GetRepository<Team, string>();
            var memberRepo = _unitOfWork.GetRepository<TeamMember, int>();
            //var user = await _userManager.FindByIdAsync(userId)
            //                    ?? throw new UserNotFoundException(userId);

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


            var leader = new TeamMember
            {
                UserId = userId,
                TeamId = team.Id,
                CreatedOn = DateTime.UtcNow,
                IsLeader = true,
                Title = "Leader"
            };
            team.Members = new List<TeamMember> { leader };

            teamRepo.Add(team);

            await _unitOfWork.SaveChangesAsync();

            team.LeaderId = leader.Id;
            teamRepo.Update(team);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TeamResponse>(team);
        }


        //public async Task<bool> DeleteTeamAsync(string teamId)
        //{
        //    var repo = _unitOfWork.GetRepository<Team, string>();

        //    var specs = new TeamSpecification(teamId);
        //    var team = await repo.GetByIdAsync(specs)
        //        ?? throw new TeamNotFoundException(teamId);

        //    team.IsDeleted = true;
        //    repo.Update(team);
        //    return await _unitOfWork.SaveChangesAsync() > 0;

        //}

        public async Task<bool> DeleteTeamAsync(string teamId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
                throw new UserNotFoundException(userId);

            var repo = _unitOfWork.GetRepository<Team, string>();

            var team = await repo
                .GetByIdAsync(new TeamLeaderSpecification(teamId, userId))
                ?? throw new UnauthorizedAccessException("You are not authorized to delete this team.");

            team.IsDeleted = true;
            repo.Update(team);

            return await _unitOfWork.SaveChangesAsync() > 0;
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

        public async Task<TeamResponse> GetTeamByIdAsync(string teamId)
        {
            var repo = _unitOfWork.GetRepository<Team, string>();

            var specs = new TeamSpecification(teamId);
            var team = await repo.GetByIdAsync(specs)
                ?? throw new TeamNotFoundException(teamId);

            return _mapper.Map<TeamResponse>(team);
        }

        public async Task<bool> JoinTeam(string joinCode, string userId)
        {
            var teamRepo = _unitOfWork.GetRepository<Team, string>();
            var memberRepo = _unitOfWork.GetRepository<TeamMember, int>();

            var team = (await teamRepo.GetAllAsync(new TeamSpecification()))
                       .FirstOrDefault(t => t.JoinCode == joinCode && t.IsDeleted == false)
                       ?? throw new TeamNotFoundException(joinCode);

            if (team.IsLocked)
                throw new Exception("Team is locked");

            if (team.Members.Count >= team.MaxCapacity)
                throw new Exception("Team is full");

            var alreadyMember = team.Members.Any(m => m.UserId == userId);
            if (alreadyMember)
                throw new Exception("User is already a member of this team");

            var member = new TeamMember
            {
                TeamId = team.Id,
                UserId = userId,
                CreatedOn = DateTime.UtcNow,
                IsLeader = false,
                Title = "Member"
            };

            memberRepo.Add(member);

            await _unitOfWork.SaveChangesAsync();

            return true;
        }


        //public async Task<TeamResponse> UpdateTeamSettingsAsync(string teamId, string userId, TeamUpdateDto teamUpdateDto)
        //{
        //    var repo = _unitOfWork.GetRepository<Team, string>();

        //    var user = await _userManager.FindByIdAsync(userId)
        //        ?? throw new UserNotFoundException(userId);

        //    var specs = new TeamSpecification(teamId);
        //    var team = await repo.GetByIdAsync(specs)
        //        ?? throw new TeamNotFoundException(teamId);

        //    var teamMemberRepo = _unitOfWork.GetRepository<TeamMember, int>();

        //    var member = (await teamMemberRepo.GetAllAsync(new TeamMemberSpecifiaction()))
        //                .FirstOrDefault(t => t.UserId == user.Id && t.TeamId == team.Id)
        //                ?? throw new MemberNotFoundException();

        //    if (!member.IsLeader)
        //        throw new UnauthorizedAccessException("You are not authorized to update this team settings.");

        //    _mapper.Map(teamUpdateDto, team);

        //    await _unitOfWork.SaveChangesAsync();

        //    return _mapper.Map<TeamResponse>(team);
        //}

        public async Task<TeamResponse> UpdateTeamSettingsAsync(string teamId,string userId, TeamUpdateDto dto)
        {
            if(string.IsNullOrWhiteSpace(userId))
                throw new UserNotFoundException(userId);

            var repo = _unitOfWork.GetRepository<Team, string>();

            var team = await repo
                .GetByIdAsync(new TeamLeaderSpecification(teamId, userId))
                ?? throw new UnauthorizedAccessException("You are not authorized to update this team.");

            _mapper.Map(dto, team);

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
