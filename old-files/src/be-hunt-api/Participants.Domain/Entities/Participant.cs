namespace Participants.Domain.Entities
{
    public sealed class Participant
    {
        /// <summary>
        /// Gets or sets the identifier of the participant.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the participant.
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the participant.
        /// </summary>
        public required string Password { get; set; }

        /// <summary>
        /// Gets or sets the list of participations of the participant.
        /// </summary>
        public required List<Participation> Participations { get; set; }
    }
}
