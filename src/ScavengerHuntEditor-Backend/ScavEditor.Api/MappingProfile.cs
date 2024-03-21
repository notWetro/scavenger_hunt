using AutoMapper;
using ScavEditor.Api.DTOs;
using ScavEditor.Api.Models;

namespace ScavEditor.Api
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Participant, ParticipantDto>();
            CreateMap<ParticipantDto, Participant>();

            CreateMap<Participation, ParticipationDto>();
            CreateMap<ParticipationDto, Participation>();

            CreateMap<ScavengerHunt, ScavengerHuntDto>();
            CreateMap<ScavengerHuntDto, ScavengerHunt>();

            CreateMap<Station, StationDto>();
            CreateMap<StationDto, Station>();

            CreateMap<TaskBase, TaskBaseDto>();
            CreateMap<TaskBaseDto, TaskBase>();

            CreateMap<TaskQuestionAnswer, TaskQuestionAnswerDto>();
            CreateMap<TaskQuestionAnswerDto, TaskQuestionAnswer>();

            CreateMap<TaskText, TaskTextDto>();
            CreateMap<TaskTextDto, TaskText>();
        }
    }
}
