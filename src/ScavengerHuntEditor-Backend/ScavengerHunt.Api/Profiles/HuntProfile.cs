using AutoMapper;
using ScavengerHunt.Api.DTOs.Hunt;
using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Api.Profiles
{
    public class HuntProfile : Profile
    {
        public HuntProfile()
        {
            CreateMap<HuntCreateDto, Hunt>();
            CreateMap<Hunt, HuntGetDto>();
            CreateMap<HuntUpdateDto, Hunt>();
        }
    }
}
