namespace Participants.Domain.Entities
{
    public sealed class Solution
    {
        /// <summary>
        /// Gets or sets the type of the solution.
        /// </summary>
        public int SolutionType { get; set; }

        /// <summary>
        /// Gets or sets the data of the solution.
        /// </summary>
        public required string Data { get; set; }
    }
}
