namespace ScavengerHunt.Domain.Entities
{
    public sealed class Solution
    {
        public int Id { get; set; }
        public SolutionType Type { get; set; }
        public string Data { get; set; } = default!;

        public int AssignmentId { get; set; }
        public required Assignment Assignment { get; set; }
    }
}
