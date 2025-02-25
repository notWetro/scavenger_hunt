using AutoMapper;
using Hunts.Api.DTOs.Hint;
using Hunts.Domain.Entities;

namespace Hunts.Api.Profiles
{
    public sealed class HintProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HintProfile"/> class.
        /// Configures the mappings between Hint and its DTOs.
        /// </summary>
        public HintProfile()
        {
            CreateMap<Hint, HintInnerGetDto>();
            CreateMap<HintInnerUpdateDto, Hint>();
            CreateMap<HintInnerCreateDto, Hint>();
            CreateMap<Hint, HintPublishDto>();
            CreateMap<HintPublishDto, Hint>();
        }
    }
}
