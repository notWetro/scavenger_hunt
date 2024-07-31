namespace Participants.Api.DTOs.Login
{
    public sealed class UserCredentials
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
