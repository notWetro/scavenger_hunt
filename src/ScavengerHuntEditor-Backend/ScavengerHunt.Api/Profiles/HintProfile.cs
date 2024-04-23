using AutoMapper;
using ScavengerHunt.Api.DTOs.Assignment;
using ScavengerHunt.Api.DTOs.Hint;
using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Api.Profiles
{
    public class HintProfile : Profile
    {
        public HintProfile()
        {
            CreateMap<Hint, HintInnerGetDto>();
            CreateMap<HintInnerCreateDto, Hint>();
        }
    }
}
