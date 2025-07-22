namespace Participants.Api.DTOs.Login
{
    public sealed class LoginRequestDto
    {
        /// <summary>
        /// Username of the user.
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// Password of the user.
        /// </summary>
        public required string Password { get; set; }
    }
}
