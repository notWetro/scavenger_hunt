using AutoMapper;
using Hunts.Api.DTOs.Hint;
using Hunts.Domain.Entities;

namespace Hunts.Api.Profiles
{
    public sealed class HintProfile : Profile
    {
        public HintProfile()
        {
            CreateMap<Hint, HintInnerGetDto>();
            CreateMap<HintInnerCreateDto, Hint>();
        }
    }
}
