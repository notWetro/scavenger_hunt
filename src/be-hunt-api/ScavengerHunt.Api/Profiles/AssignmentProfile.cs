using AutoMapper;
using ScavengerHunt.Api.DTOs.Assignment;
using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Api.Profiles
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
