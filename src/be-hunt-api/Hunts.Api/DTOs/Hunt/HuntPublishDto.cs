using Hunts.Api.DTOs.Assignment;

namespace Hunts.Api.DTOs.Hunt
{
    public sealed class HuntPublishDto
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required ICollection<AssignmentPublishDto> Assignments { get; set; }

        public required string Event { get; set; }
    }
}
