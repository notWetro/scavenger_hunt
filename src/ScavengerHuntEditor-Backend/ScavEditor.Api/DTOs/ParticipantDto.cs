using Newtonsoft.Json;

namespace ScavEditor.Api.DTOs
{
    public sealed class ParticipantDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; } = string.Empty;

        [JsonProperty("participations")]
        public List<ParticipationDto> Participations { get; set; } = [];
    }
}
