using Participants.Domain.Entities;

namespace Participants.Api.DTOs.Login
{
    public sealed class HuntLoginDto
    {
        /// <summary>
        /// Identifier of the hunt.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Title of the hunt.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Participation status of the hunt.
        /// </summary>
        public required ParticipationStatus ParticipationStatus { get; set; }
    }
}
