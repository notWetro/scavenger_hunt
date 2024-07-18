using Hunts.Api.DTOs.Assignment;

namespace Hunts.Api.DTOs.Hunt
{
    public sealed class HuntGetDto
    {
        public int Id { get; set; }

        public required string Title { get; set; }

        public required string Description { get; set; }

        public ICollection<AssignmentInnerGetDto> Assignments { get; set; } = [];
    }
}
