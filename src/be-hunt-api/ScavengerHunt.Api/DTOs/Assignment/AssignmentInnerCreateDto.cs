using ScavengerHunt.Api.DTOs.Hint;
using ScavengerHunt.Api.DTOs.Solution;

namespace ScavengerHunt.Api.DTOs.Assignment
{
    public sealed class AssignmentInnerCreateDto
    {
        public required HintInnerCreateDto Hint { get; set; }

        public required SolutionInnerCreateDto Solution { get; set; }
    }
}
