namespace Participants.Api.DTOs.CurrentAssignment
{
    public sealed class CurrentAssignmentResponseDto
    {
        public int HintType { get; set; }
        public required string HintData { get; set; }
        public int SolutionType { get; set; }
        
        public string? additionalData {get; set;}
    }
}
