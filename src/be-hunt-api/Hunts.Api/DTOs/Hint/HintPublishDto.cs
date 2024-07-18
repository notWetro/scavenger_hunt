using Hunts.Domain;

namespace Hunts.Api.DTOs.Hint
{
    public sealed class HintPublishDto
    {
        public HintType HintType { get; set; }
        public required string Data { get; set; }
    }
}
