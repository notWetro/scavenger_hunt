using ScavengerHunt.Domain;

namespace ScavengerHunt.Api.DTOs.Solution
{
    public class SolutionInnerGetDto
    {
        public int Id { get; set; }
        public SolutionType Type { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}
