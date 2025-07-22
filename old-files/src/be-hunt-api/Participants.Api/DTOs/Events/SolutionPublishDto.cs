namespace Participants.Api.DTOs.Events
{
    public sealed class SolutionPublishDto
    {
        /// <summary>
        /// Type of the solution.
        /// </summary>
        public int SolutionType { get; set; }

        /// <summary>
        /// Data of the solution.
        /// </summary>
        public required string Data { get; set; }
    }
}
