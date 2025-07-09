using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Domain.Models.Teams;
using Microsoft.AspNetCore.Identity;
using Services.Specifications;
using ServicesAbstraction;
using Shared.DataTransferObjects.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TeamService(IUnitOfWork _unitOfWork,
                             IMapper _mapper,
                             UserManager<ApplicationUser> _userManager) : ITeamService
    {
        public async Task<TeamResponse> CreateTeamAsync(TeamCreationDto teamCreationDto, string mail)
        {
            var team = _mapper.Map<Team>(teamCreationDto)
                ?? throw new Exception("Can't Create This Team");

            var teamRepo = _unitOfWork.GetRepository<Team, string>();
            var memberRepo = _unitOfWork.GetRepository<TeamMember, int>();
            var user = await _userManager.FindByEmailAsync(mail);

            team.Id = Guid.NewGuid().ToString();
            team.CreatedOn = DateTime.UtcNow;
            team.MaxCapacity = team.MaxCapacity == 0 ? 50 : teamCreationDto.MaxCapacity;

            string joinCode;
            do
            {
                joinCode = GenerateRandomCode();
            } while ((await teamRepo.GetAllAsync(new TeamSpecification())).Any(t => t.JoinCode == joinCode));
            team.JoinCode = joinCode;

            teamRepo.Add(team);
            await _unitOfWork.SaveChangesAsync();

            var teamMember = new TeamMember
            {
                UserId = user!.Id,
                TeamId = team.Id,
                CreatedOn = DateTime.UtcNow,
                IsLeader = true,
                Title = user.FirstName
            };

            memberRepo.Add(teamMember);
            await _unitOfWork.SaveChangesAsync();

            var leaderId = teamMember.Id;
            team.LeaderId = leaderId;

            teamRepo.Update(team);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TeamResponse>(team)
                   ?? throw new Exception("Can't Map This Team to Response DTO");
        }


        public Task<bool> DeleteTeamAsync(string teamId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TeamResponse>> GetAllTeamsByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<TeamResponse> GetTeamByIdAsync(string teamId)
        {
            throw new NotImplementedException();
        }

        public Task<TeamResponse> UpdateTeamSettingsAsync(TeamUpdateDto teamUpdateDto)
        {
            throw new NotImplementedException();
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
