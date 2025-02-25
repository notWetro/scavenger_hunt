namespace Hunts.Domain.Entities
{
    /// <summary>
    /// Represents a solution for an assignment.
    /// </summary>
    public sealed class Solution
    {
        public int Id { get; set; }
        public SolutionType SolutionType { get; set; }
        public string Data { get; set; } = default!;

        public int AssignmentId { get; set; }
        public required Assignment Assignment { get; set; }
    }
}
