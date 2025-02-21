using Hunts.Api.DTOs.Assignment;

namespace Hunts.Api.DTOs.Hunt
{
    public sealed class HuntUpdateDto
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
        /// A description usually contains additional information of a hunt.
        /// </summary>
        public required string Description { get; set; }

        /// <summary>
        /// A collection of assignments related to the hunt.
        /// </summary>
        public ICollection<AssignmentInnerUpdateDto> Assignments { get; set; } = [];
    }
}
