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
            CreateMap<AssignmentInnerCreateDto, Assignment>();
        }
    }
}
