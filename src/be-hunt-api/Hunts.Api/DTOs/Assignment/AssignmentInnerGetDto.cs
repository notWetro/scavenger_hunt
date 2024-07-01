using Hunts.Api.DTOs.Hint;
using Hunts.Api.DTOs.Solution;

namespace Hunts.Api.DTOs.Assignment
{
    public sealed class AssignmentInnerGetDto
    {
        public int Id { get; set; }

        public required HintInnerGetDto Hint { get; set; }

        public required SolutionInnerGetDto Solution { get; set; }
    }
}
