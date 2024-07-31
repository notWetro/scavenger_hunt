namespace Participants.Api.DTOs.Participation
{
    public sealed class ParticipationStatsDto
    {
        public required int ParticipantCount { get; set; }
        public required int ParticipationCount { get; set; }
    }
}
