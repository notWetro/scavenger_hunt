namespace Participants.Api.DTOs.Events
{
    public sealed class HuntPublishDto : GenericEventDto
    {
        /// <summary>
        /// Identifier of the hunt.
        /// </summary>
        public required int Id { get; set; }

        /// <summary>
        /// Title of the hunt.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// List of assignments of the hunt.
        /// </summary>
        public required ICollection<AssignmentPublishDto> Assignments { get; set; }
    }
}
