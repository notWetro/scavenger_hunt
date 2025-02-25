namespace Participants.Domain.Entities
{
    public sealed class Hunt
    {
        /// <summary>
        /// Gets or sets the identifier of the hunt.
        /// </summary>
        public required int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the hunt.
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// Gets or sets the collection of assignments in the hunt.
        /// </summary>
        public required ICollection<Assignment> Assignments { get; set; }
    }
}
