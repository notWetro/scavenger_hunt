namespace Participants.Api.DTOs.SubmitSolution
{
    public sealed class SubmitSolutionResponseDto
    {
        /// <summary>
        /// Gets or sets a value indicating whether the solution submission was successful.
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the hint data related to the solution submission.
        /// </summary>
        public required string HintData { get; set; }
    }
}
