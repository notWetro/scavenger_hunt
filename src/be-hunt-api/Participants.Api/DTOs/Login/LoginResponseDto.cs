namespace Participants.Api.DTOs.Login
{
    public sealed class LoginResponseDto
    {
        public required string Token { get; set; }
        public ICollection<HuntLoginDto> Hunts { get; set; } = [];
    }
}
