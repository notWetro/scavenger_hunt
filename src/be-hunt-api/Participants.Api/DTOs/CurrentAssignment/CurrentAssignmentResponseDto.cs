namespace Participants.Api.DTOs.CurrentAssignment
{
    public sealed class CurrentAssignmentResponseDto
    {
        /// <summary>
        /// Type of the hint.
        /// </summary>
        public int HintType { get; set; }

        /// <summary>
        /// Data of the hint.
        /// </summary>
        public required string HintData { get; set; }

        /// <summary>
        /// Type of the solution.
        /// </summary>
        public int SolutionType { get; set; }   

        /// <summary>
        /// Additional data.
        /// </summary>
        public string? AdditionalData {get; set;}
    }
}
