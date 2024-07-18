namespace Participants.Api.DTOs.Events
{
    public sealed class SolutionPublishDto
    {
        public int SolutionType { get; set; }
        public required string Data { get; set; }
    }
}
