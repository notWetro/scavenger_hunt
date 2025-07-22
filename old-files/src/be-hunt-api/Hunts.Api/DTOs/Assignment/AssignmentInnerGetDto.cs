using Hunts.Api.DTOs.Hint;
using Hunts.Api.DTOs.Solution;

namespace Hunts.Api.DTOs.Assignment
{
    public sealed class AssignmentInnerGetDto
    {
        /// <summary>
        /// Gets or sets the Id of the assignment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the hint associated with the assignment.
        /// </summary>
        public required HintInnerGetDto Hint { get; set; }

        /// <summary>
        /// Gets or sets the solution associated with the assignment.
        /// </summary>
        public required SolutionInnerGetDto Solution { get; set; }
    }
}
