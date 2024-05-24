using AutoMapper;
using ScavengerHunt.Api.DTOs.Solution;
using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Api.Profiles
{
    public sealed class SolutionProfile : Profile
    {
        public SolutionProfile()
        {
            CreateMap<Solution, SolutionInnerGetDto>();
            CreateMap<SolutionInnerCreateDto, Solution>();
        }
    }
}
