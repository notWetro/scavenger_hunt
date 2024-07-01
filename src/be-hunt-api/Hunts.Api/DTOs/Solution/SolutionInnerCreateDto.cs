using Hunts.Domain;

namespace Hunts.Api.DTOs.Solution
{
    public sealed class SolutionInnerCreateDto
    {
        public required SolutionType Type { get; set; }
        public required string Data { get; set; }
    }
}
