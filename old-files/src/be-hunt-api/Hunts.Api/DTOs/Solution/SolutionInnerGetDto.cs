using Hunts.Domain;

namespace Hunts.Api.DTOs.Solution
{
    public sealed class SolutionInnerGetDto
    {
        /// <summary>
        /// Gets or sets the ID of the solution.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the type of the solution.
        /// </summary>
        public required SolutionType SolutionType { get; set; }

        /// <summary>
        /// Gets or sets the data of the solution.
        /// </summary>
        public required string Data { get; set; }
    }
}
