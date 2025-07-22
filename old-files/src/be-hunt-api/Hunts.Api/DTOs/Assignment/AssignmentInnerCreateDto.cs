using Hunts.Api.DTOs.Hint;
using Hunts.Api.DTOs.Solution;

namespace Hunts.Api.DTOs.Assignment
{
    public sealed class AssignmentInnerCreateDto
    {
        /// <summary>
        /// Gets or sets the hint to be created for the assignment.
        /// </summary>
        public required HintInnerCreateDto Hint { get; set; }

        /// <summary>
        /// Gets or sets the solution to be created for the assignment.
        /// </summary>
        public required SolutionInnerCreateDto Solution { get; set; }
    }
}
