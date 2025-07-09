using AutoMapper;
using Domain.Models.Teams;
using Shared.DataTransferObjects.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class TeamProfile : Profile
    {

        public TeamProfile()
        {
            CreateMap<TeamCreationDto, Team>().ReverseMap();
            CreateMap<TeamResponse, Team>().ReverseMap();
        }

    }
}
