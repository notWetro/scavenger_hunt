namespace Participants.Domain.Entities
{
    public sealed class Hunt
    {
        public int Id { get; set; }
        public required ICollection<int> Assignments { get; set; }
    }
}
