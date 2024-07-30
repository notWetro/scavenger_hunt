using AutoMapper;
using Hunts.Api.DTOs.Assignment;
using Hunts.Domain.Entities;

namespace Hunts.Api.Profiles
{
    public sealed class AssignmentProfile : Profile
    {
        public AssignmentProfile()
        {
            CreateMap<Assignment, AssignmentInnerGetDto>();
            CreateMap<AssignmentInnerUpdateDto, Assignment>();
            CreateMap<AssignmentInnerCreateDto, Assignment>();
            CreateMap<Assignment, AssignmentPublishDto>();
            CreateMap<AssignmentPublishDto, Assignment>();
        }
    }
}
