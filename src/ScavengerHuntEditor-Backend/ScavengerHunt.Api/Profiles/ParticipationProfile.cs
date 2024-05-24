using AutoMapper;
using ScavengerHunt.Api.DTOs.Participation;
using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Api.Profiles
{
    public sealed class ParticipationProfile : Profile
    {
        public ParticipationProfile()
        {
            CreateMap<Participation, ParticipationGetDto>();
            CreateMap<ParticipationGetDto, Participation>();
        }
    }
}
