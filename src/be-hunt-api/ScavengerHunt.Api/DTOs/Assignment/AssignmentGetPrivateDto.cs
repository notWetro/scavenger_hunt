using ScavengerHunt.Api.DTOs.Hint;
using ScavengerHunt.Api.DTOs.Solution;

namespace ScavengerHunt.Api.DTOs.Assignment
{
    public sealed class AssignmentGetPrivateDto
    {
        public int Id { get; set; }

        public required HintInnerGetDto Hint { get; set; }

        public required SolutionGetPrivateDto Solution { get; set; }
    }
}
