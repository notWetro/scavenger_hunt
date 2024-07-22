namespace Participants.Domain.Entities
{
    public sealed class Participation
    {
        public int Id { get; set; }

        public int ParticipantId { get; set; }
        public required Participant Participant { get; set; }

        public int HuntId { get; set; }

        public int CurrentAssignmentId { get; set; }

        public required ParticipationStatus Status { get; set; }
    }

    public enum ParticipationStatus
    {
        Invalid,
        Deleted,
        Running,
        Finished,
        Stopped,
    }
}
