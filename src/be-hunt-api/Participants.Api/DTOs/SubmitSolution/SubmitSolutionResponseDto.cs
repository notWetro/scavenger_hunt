namespace Participants.Api.DTOs.SubmitSolution
{
    public sealed class SubmitSolutionResponseDto
    {
        public bool Success { get; set; }
        public required string HintData { get; set; }
    }
}
