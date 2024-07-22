namespace Participants.Api.DTOs.Login
{
    public sealed class LoginRequestDto
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
