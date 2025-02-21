using Hunts.Api.DTOs.Assignment;

namespace Hunts.Api.DTOs.Hunt
{
    public sealed class HuntCreateDto
    {
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
        public ICollection<AssignmentInnerCreateDto> Assignments { get; set; } = [];
    }
}
