using AutoMapper;
using Hunts.Api.DTOs.Solution;
using Hunts.Domain.Entities;

namespace Hunts.Api.Profiles
{
    public sealed class SolutionProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionProfile"/> class.
        /// Configures the mappings between Solution and its DTOs.
        /// </summary>
        public SolutionProfile()
        {
            CreateMap<Solution, SolutionInnerGetDto>();
            CreateMap<SolutionInnerUpdateDto, Solution>();
            CreateMap<SolutionInnerCreateDto, Solution>();
            CreateMap<Solution, SolutionPublishDto>();
            CreateMap<SolutionPublishDto, Solution>();
        }
    }
}
