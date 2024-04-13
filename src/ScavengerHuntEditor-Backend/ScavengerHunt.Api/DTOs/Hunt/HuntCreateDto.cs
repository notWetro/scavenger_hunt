using ScavengerHunt.Api.DTOs.Station;

namespace ScavengerHunt.Api.DTOs.Hunt
{
    public class HuntCreateDto
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<StationInnerCreateDto> Stations { get; set; } = [];
    }
}
