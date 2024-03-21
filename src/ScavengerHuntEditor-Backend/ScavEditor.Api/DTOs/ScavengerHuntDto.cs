using Newtonsoft.Json;

namespace ScavEditor.Api.DTOs
{
    public sealed class ScavengerHuntDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        //[JsonProperty("stations")]
        //public List<StationDto> Stations { get; set; } = [];

        //[JsonProperty("participations")]
        //public List<ParticipationDto> Participations { get; set; } = [];
    }
}
