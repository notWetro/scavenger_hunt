using Hunts.Domain;

namespace Hunts.Api.DTOs.Hint
{
    public sealed class HintInnerGetDto
    {
        /// <summary>
        /// The ID of the hint.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The type of the hint.
        /// </summary>
        public required HintType HintType { get; set; }

        /// <summary>
        /// The data of the hint.
        /// </summary>
        public required string Data { get; set; }

        /// <summary>
        /// Additional data of the hint.
        /// </summary>
        public string? additionalData {get; set;}
    }
}
