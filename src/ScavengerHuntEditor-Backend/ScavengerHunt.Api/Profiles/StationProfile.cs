using AutoMapper;
using ScavengerHunt.Api.DTOs.Station;
using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Api.Profiles
{
    public class StationProfile : Profile
    {
        public StationProfile()
        {
            CreateMap<StationInnerCreateDto, Station>();
        }
    }
}
