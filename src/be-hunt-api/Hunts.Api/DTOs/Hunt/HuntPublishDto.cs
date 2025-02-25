using Hunts.Api.DTOs.Assignment;

namespace Hunts.Api.DTOs.Hunt
{
    public sealed class HuntPublishDto
    {
        /// <summary>
        /// Unique identifier of a hunt.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A title usually contains a topic of a hunt.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// A collection of assignments related to the hunt.
        /// </summary>
        public required ICollection<AssignmentPublishDto> Assignments { get; set; }

        /// <summary>
        /// The event associated with the hunt.
        /// </summary>
        public required string Event { get; set; }
    }
}
