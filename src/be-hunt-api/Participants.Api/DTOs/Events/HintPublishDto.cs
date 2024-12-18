namespace Participants.Api.DTOs.Events
{
    public sealed class HintPublishDto
    {
        public int HintType { get; set; }
        public required string Data { get; set; }
        public string? additionalData {get; set;}
    }
}
