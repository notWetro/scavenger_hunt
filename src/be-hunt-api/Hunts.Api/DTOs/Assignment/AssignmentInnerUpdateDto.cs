using Hunts.Api.DTOs.Hint;
using Hunts.Api.DTOs.Solution;

namespace Hunts.Api.DTOs.Assignment
{
    public sealed class AssignmentInnerUpdateDto
    {
        /// <summary>
        /// Gets or sets the hint to be updated for the assignment.
        /// </summary>
        public required HintInnerUpdateDto Hint { get; set; }

        /// <summary>
        /// Gets or sets the solution to be updated for the assignment.
        /// </summary>
        public required SolutionInnerUpdateDto Solution { get; set; }
    }
}
