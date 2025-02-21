using AutoMapper;
using Participants.Api.DTOs.Participation;
using Participants.Domain.Entities;

namespace Participants.Api.Profiles
{
    public sealed class ParticipationProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParticipationProfile"/> class.
        /// </summary>
        public ParticipationProfile()
        {
            CreateMap<Participation, ParticipationGetDto>();
            CreateMap<ParticipationGetDto, Participation>();
        }
    }
}
