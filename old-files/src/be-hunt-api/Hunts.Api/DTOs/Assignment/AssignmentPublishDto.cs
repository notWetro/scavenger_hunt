using Hunts.Api.DTOs.Hint;
using Hunts.Api.DTOs.Solution;

namespace Hunts.Api.DTOs.Assignment
{
    public sealed class AssignmentPublishDto
    {
        /// <summary>
        /// Gets or sets the Id of the assignment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the hint to be published for the assignment.
        /// </summary>
        public required HintPublishDto Hint { get; set; }

        /// <summary>
        /// Gets or sets the solution to be published for the assignment.
        /// </summary>
        public required SolutionPublishDto Solution { get; set; }
    }
}
