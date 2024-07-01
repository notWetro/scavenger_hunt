using AutoMapper;
using Participants.Api.DTOs.Participation;
using Participants.Domain.Entities;

namespace Participants.Api.Profiles
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
