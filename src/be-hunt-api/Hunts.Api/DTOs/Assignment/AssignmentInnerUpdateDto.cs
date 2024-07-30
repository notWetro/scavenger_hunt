using Hunts.Api.DTOs.Hint;
using Hunts.Api.DTOs.Solution;

namespace Hunts.Api.DTOs.Assignment
{
    public sealed class AssignmentInnerUpdateDto
    {
        public int Id { get; set; }

        public required HintInnerUpdateDto Hint { get; set; }

        public required SolutionInnerUpdateDto Solution { get; set; }
    }
}
