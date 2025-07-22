using AutoMapper;
using Hunts.Api.DTOs.Assignment;
using Hunts.Domain.Entities;

namespace Hunts.Api.Profiles
{
    public sealed class AssignmentProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssignmentProfile"/> class.
        /// Configures the mappings between Assignment and its DTOs.
        /// </summary>
        public AssignmentProfile()
        {
            CreateMap<Assignment, AssignmentInnerGetDto>();
            CreateMap<AssignmentInnerUpdateDto, Assignment>();
            CreateMap<AssignmentInnerCreateDto, Assignment>();
            CreateMap<Assignment, AssignmentPublishDto>();
            CreateMap<AssignmentPublishDto, Assignment>();
        }
    }
}
