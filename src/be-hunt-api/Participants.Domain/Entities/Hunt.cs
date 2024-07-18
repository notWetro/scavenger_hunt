namespace Participants.Domain.Entities
{
    public sealed class Hunt
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required ICollection<Assignment> Assignments { get; set; }
    }
}
