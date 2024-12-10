namespace Participants.Domain.Entities
{
    public sealed class Hint
    {
        public int HintType { get; set; }
        public required string Data { get; set; }

        public string? additionalData {get; set;}
    }
}
