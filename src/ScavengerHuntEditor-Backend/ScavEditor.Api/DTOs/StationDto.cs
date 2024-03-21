using Newtonsoft.Json;

namespace ScavEditor.Api.DTOs
{
    public sealed class StationDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        // Note: Instead of Latitude and Longitude we might use Points from
        // NetTopologySuite.Geometries.Point Namespace (for calculating distances, etc.)

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        //[JsonProperty("tasks")]
        //public List<TaskBaseDto> Tasks { get; set; } = [];

        [JsonProperty("idScavengerHunt")]
        public int IdScavengerHunt { get; set; }

        //[JsonProperty("associatedScavengerHunt")]
        //public ScavengerHuntDto? AssociatedScavengerHunt { get; set; }
    }
}
