using Newtonsoft.Json;
using ScavEditor.Api.Data;

namespace ScavEditor.Api.DTOs
{
    public sealed class ParticipationDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("progressionStatus")]
        public ProgressionStatus ProgressionStatus { get; set; }

        [JsonProperty("idParticipant")]
        public int IdParticipant { get; set; }

        [JsonProperty("participant")]
        public ParticipantDto? Participant { get; set; }

        [JsonProperty("idScavengerHunt")]
        public int IdScavengerHunt { get; set; }

        [JsonProperty("scavengerHunt")]
        public ScavengerHuntDto? ScavengerHunt { get; set; }

        [JsonProperty("idCurrentTask")]
        public int IdCurrentTask { get; set; }

        [JsonProperty("currentTask")]
        public TaskDto? CurrentTask { get; set; }
    }
}
