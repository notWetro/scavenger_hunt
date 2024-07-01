using Hunts.Api.DTOs.Hint;
using Hunts.Api.DTOs.Solution;

namespace Hunts.Api.DTOs.Assignment
{
    public sealed class AssignmentInnerCreateDto
    {
        public required HintInnerCreateDto Hint { get; set; }

        public required SolutionInnerCreateDto Solution { get; set; }
    }
}
