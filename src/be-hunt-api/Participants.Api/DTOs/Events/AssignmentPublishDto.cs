namespace Participants.Api.DTOs.Events
{
    public sealed class AssignmentPublishDto
    {
        public int Id { get; set; }
        public required HintPublishDto Hint { get; set; }
        public required SolutionPublishDto Solution { get; set; }
    }
}
