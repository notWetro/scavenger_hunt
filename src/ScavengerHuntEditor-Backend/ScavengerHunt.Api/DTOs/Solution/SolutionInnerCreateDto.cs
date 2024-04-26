using ScavengerHunt.Domain;

namespace ScavengerHunt.Api.DTOs.Solution
{
    public class SolutionInnerCreateDto
    {
        public SolutionType Type { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}
