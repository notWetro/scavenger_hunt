using Hunts.Domain;

namespace Hunts.Api.DTOs.Solution
{
    public sealed class SolutionInnerUpdateDto
    {
        public int Id { get; set; }
        public required SolutionType SolutionType { get; set; }
        public required string Data { get; set; }
    }
}
