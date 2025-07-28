using AutoMapper;
using Domain.Models.Teams;
using Shared.DataTransferObjects.TeamMembers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class TeamMemberProfile : Profile
    {

        public TeamMemberProfile()
        {
            CreateMap<TeamMember, TeamMemberResponse>().ReverseMap();
        }

    }
}
