namespace Participants.Api.DTOs.Events
{
    public sealed class HuntPublishDto : GenericEventDto
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required ICollection<AssignmentPublishDto> Assignments { get; set; }
    }
}
