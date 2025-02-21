namespace Participants.Api.DTOs.Participation
{
    public sealed class ParticipationStatsDto
    {
        /// <summary>
        /// Gets or sets the count of participants.
        /// </summary>
        public required int ParticipantCount { get; set; }

        /// <summary>
        /// Gets or sets the count of participations.
        /// </summary>
        public required int ParticipationCount { get; set; }
    }
}
