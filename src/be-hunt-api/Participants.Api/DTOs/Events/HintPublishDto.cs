namespace Participants.Api.DTOs.Events
{
    public sealed class HintPublishDto
    {
        /// <summary>
        /// Type of the hint.
        /// </summary>
        public int HintType { get; set; }

        /// <summary>
        /// Data of the hint.
        /// </summary>
        public required string Data { get; set; }

        /// <summary>
        /// Additional data of the hint.
        /// </summary>
        public string? additionalData {get; set;}
    }
}
