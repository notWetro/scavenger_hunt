using ScavengerHunt.Api.DTOs.Hint;
using ScavengerHunt.Api.DTOs.Solution;
using ScavengerHunt.Domain.Entities;

namespace ScavengerHunt.Api.DTOs.Assignment
{
    public sealed class AssignmentInnerGetDto
    {
        public int Id { get; set; }

        public HintInnerGetDto Hint { get; set; }

        public SolutionInnerGetDto Solution { get; set; }
    }
}
