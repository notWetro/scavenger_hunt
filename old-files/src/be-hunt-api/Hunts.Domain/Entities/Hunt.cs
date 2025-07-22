namespace Hunts.Domain.Entities
{
    /// <summary>
    /// Represents a hunt with multiple assignments.
    /// </summary>
    public sealed class Hunt
    {
        /// <summary>
        /// Unique identifier of a hunt.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// A title usually contains a topic of a hunt.
        /// </summary>
        public string Title { get; set; } = default!;

        /// <summary>
        /// A description usually contains additional information of a hunt.
        /// </summary>
        public string Description { get; set; } = default!;

        /// <summary>
        /// Each hunt has multiple tasks to complete.
        /// </summary>
        public ICollection<Assignment> Assignments { get; set; } = [];
    }
}
