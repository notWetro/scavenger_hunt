using Hunts.Domain;

namespace Hunts.Api.DTOs.Hint
{
    public sealed class HintInnerGetDto
    {
        public int Id { get; set; }
        public required HintType HintType { get; set; }
        public required string Data { get; set; }
        public string? additionalData {get; set;}

    }
}
