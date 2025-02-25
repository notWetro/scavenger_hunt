namespace Participants.Api.DTOs.Login
{
    public sealed class UserCredentials
    {
        /// <summary>
        /// Gets or sets the username of the participant.
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// Gets or sets the password of the participant.
        /// </summary>
        public required string Password { get; set; }
    }
}
