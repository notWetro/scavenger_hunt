using Hunts.Api.DTOs.Assignment;

namespace Hunts.Api.DTOs.Hunt
{
    public sealed class HuntCreateDto
    {
        public required string Title { get; set; }

        public required string Description { get; set; }

        public ICollection<AssignmentInnerCreateDto> Assignments { get; set; } = [];
    }
}
