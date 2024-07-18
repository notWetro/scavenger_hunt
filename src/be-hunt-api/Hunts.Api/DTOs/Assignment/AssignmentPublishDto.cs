using Hunts.Api.DTOs.Hint;
using Hunts.Api.DTOs.Solution;

namespace Hunts.Api.DTOs.Assignment
{
    public sealed class AssignmentPublishDto
    {
        public int Id { get; set; }

        public required HintPublishDto Hint { get; set; }

        public required SolutionPublishDto Solution { get; set; }
    }
}
