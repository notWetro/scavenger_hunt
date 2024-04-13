using AutoMapper;
using ScavengerHunt.Api.DTOs.Task;
using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Api.Profiles
{
    public class TaskProfile : Profile
    {
        public TaskProfile() 
        {
            CreateMap<TaskTextInnerCreateDto, TaskText>();
            CreateMap<TaskText, TaskTextInnerGetDto>();
        }
    }
}
