using ScavengerHunt.Api.DTOs.Hint;
using ScavengerHunt.Api.DTOs.Solution;

namespace ScavengerHunt.Api.DTOs.Assignment
{
    public class AssignmentInnerCreateDto
    {
        public HintInnerCreateDto Hint { get; set; }

        public SolutionInnerCreateDto Solution { get; set; }
    }
}
