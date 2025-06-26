using AutoMapper;
using Domain.Models;
using Shared.DataTransferObjects.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task = Domain.Models.Task;
namespace Services.MappingProfiles
{
    public class TaskProfile : Profile
    {
        protected TaskProfile()
        {

            CreateMap<Task, TaskResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority)).ReverseMap();

        }
    }
}
