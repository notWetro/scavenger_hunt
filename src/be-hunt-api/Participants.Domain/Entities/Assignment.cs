

namespace Participants.Domain.Entities
{
    public sealed class Assignment
    {
        public int Id { get; set; }
        public required Hint Hint { get; set; }
        public required Solution Solution { get; set; }
    }
}
