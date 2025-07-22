namespace Participants.Domain.Entities
{
    public sealed class Assignment
    {
        /// <summary>
        /// Gets or sets the identifier of the assignment.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the hint of the assignment.
        /// </summary>
        public required Hint Hint { get; set; }

        /// <summary>
        /// Gets or sets the solution of the assignment.
        /// </summary>
        public required Solution Solution { get; set; }
    }
}
