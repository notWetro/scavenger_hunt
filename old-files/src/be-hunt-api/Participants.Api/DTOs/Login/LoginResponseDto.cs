namespace Participants.Api.DTOs.Login
{
    public sealed class LoginResponseDto
    {
        /// <summary>
        /// Gets or sets the JWT token.
        /// </summary>
        public required string Token { get; set; }

        /// <summary>
        /// Gets or sets the collection of hunts associated with the participant.
        /// </summary>
        public ICollection<HuntLoginDto> Hunts { get; set; } = [];
    }
}
