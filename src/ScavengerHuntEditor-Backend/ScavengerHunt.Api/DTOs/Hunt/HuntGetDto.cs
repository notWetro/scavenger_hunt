using ScavengerHunt.Api.DTOs.Station;

namespace ScavengerHunt.Api.DTOs.Hunt
{
    public class HuntGetDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Every hunt consists of multiple stations.
        /// TODO: Ask if aggregation-encapsulation makes sense here.
        /// Private: Stations cannot be added from outside directly but only with AddStation() Method
        /// </summary>
        public ICollection<StationInnerGetDto> Stations { get; set; } = [];
    }
}
