namespace Participants.Api.DTOs.Events
{
    public sealed class AssignmentPublishDto
    {
        /// <summary>
        /// Identifier of the assignment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Hint of the assignment.
        /// </summary>
        public required HintPublishDto Hint { get; set; }

        /// <summary>
        /// Solution of the assignment.
        /// </summary>
        public required SolutionPublishDto Solution { get; set; }
    }
}
