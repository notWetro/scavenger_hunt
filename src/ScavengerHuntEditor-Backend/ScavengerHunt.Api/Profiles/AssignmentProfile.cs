using AutoMapper;
using ScavengerHunt.Api.DTOs.Assignment;
using ScavengerHunt.Api.DTOs.Hunt;
using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Api.Profiles
{
    public class AssignmentProfile : Profile
    {
        public AssignmentProfile()
        {
            CreateMap<Assignment, AssignmentInnerGetDto>();
            CreateMap<AssignmentInnerCreateDto, Assignment>();
        }
    }
}
