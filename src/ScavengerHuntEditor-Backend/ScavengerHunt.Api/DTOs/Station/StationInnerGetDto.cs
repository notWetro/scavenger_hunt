using ScavengerHunt.Api.DTOs.Task;
using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Api.DTOs.Station
{
    public class StationInnerGetDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public ICollection<TaskTextInnerGetDto> Tasks { get; set; } = [];
    }
}
