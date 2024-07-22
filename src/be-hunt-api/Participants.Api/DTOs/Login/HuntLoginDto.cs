using Participants.Domain.Entities;

namespace Participants.Api.DTOs.Login
{
    public sealed class HuntLoginDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required ParticipationStatus ParticipationStatus { get; set; }
    }
}
