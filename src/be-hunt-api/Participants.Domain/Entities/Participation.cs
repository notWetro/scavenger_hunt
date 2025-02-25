namespace Participants.Domain.Entities
{
    public sealed class Participation
    {
        /// <summary>
        /// Gets or sets the identifier of the participation.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the participant.
        /// </summary>
        public int ParticipantId { get; set; }

        /// <summary>
        /// Gets or sets the participant.
        /// </summary>
        public required Participant Participant { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the hunt.
        /// </summary>
        public int HuntId { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the current assignment.
        /// </summary>
        public int CurrentAssignmentId { get; set; }

        /// <summary>
        /// Gets or sets the status of the participation.
        /// </summary>
        public required ParticipationStatus Status { get; set; }
    }

    /// <summary>
    /// Represents the status of a participation.
    /// </summary>
    public enum ParticipationStatus
    {
        Invalid,
        Deleted,
        Running,
        Finished,
        Stopped,
    }
}
