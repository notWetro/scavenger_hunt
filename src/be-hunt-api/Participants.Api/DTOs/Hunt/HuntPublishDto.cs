using Participants.Api.DTOs.Assignment;

namespace Participants.Api.DTOs.Hunt
{
    public sealed class HuntPublishDto : GenericEventDto
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required ICollection<AssignmentPublishDto> Assignments { get; set; }
    }
}
