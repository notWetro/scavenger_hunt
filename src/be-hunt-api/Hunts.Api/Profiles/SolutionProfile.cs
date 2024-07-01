using AutoMapper;
using Hunts.Api.DTOs.Solution;
using Hunts.Domain.Entities;

namespace Hunts.Api.Profiles
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
