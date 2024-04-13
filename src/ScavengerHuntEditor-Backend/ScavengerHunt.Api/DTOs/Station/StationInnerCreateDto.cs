using ScavengerHunt.Api.DTOs.Task;

namespace ScavengerHunt.Api.DTOs.Station
{
    public class StationInnerCreateDto
    {
        public string Name { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ICollection<TaskTextInnerCreateDto> Tasks { get; set; } = [];
    }
}
