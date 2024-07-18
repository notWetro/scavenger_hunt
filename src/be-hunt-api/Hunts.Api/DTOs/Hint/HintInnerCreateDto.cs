using Hunts.Domain;

namespace Hunts.Api.DTOs.Hint
{
    public sealed class HintInnerCreateDto
    {
        public required HintType HintType { get; set; }
        public required string Data { get; set; }
    }
}
