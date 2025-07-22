using AutoMapper;
using Hunts.Api.DTOs.Hunt;
using Hunts.Domain.Entities;

namespace Hunts.Api.Profiles
{
    public sealed class HuntProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HuntProfile"/> class.
        /// Configures the mappings between Hunt and its DTOs.
        /// </summary>
        public HuntProfile()
        {
            CreateMap<HuntCreateDto, Hunt>();
            CreateMap<Hunt, HuntGetDto>();
            CreateMap<HuntUpdateDto, Hunt>();
            CreateMap<HuntPublishDto, Hunt>();
            CreateMap<Hunt, HuntPublishDto>();
        }
    }
}
