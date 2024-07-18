using Hunts.Domain;

namespace Hunts.Api.DTOs.Solution
{
    public sealed class SolutionPublishDto
    {
        public SolutionType SolutionType { get; set; }
        public required string Data { get; set; }
    }
}
