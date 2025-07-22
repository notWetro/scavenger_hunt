using Hunts.Domain;

namespace Hunts.Api.DTOs.Solution
{
    public sealed class SolutionInnerCreateDto
    {
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
