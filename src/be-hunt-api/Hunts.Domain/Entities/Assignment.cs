namespace Hunts.Domain.Entities
{
    public sealed class Assignment
    {
        public int Id { get; set; }

        public int HuntId { get; set; }
        public required Hunt Hunt { get; set; }

        public int HintId { get; set; }
        public required Hint Hint { get; set; }

        public int SolutionId { get; set; }
        public required Solution Solution { get; set; }

    }
}
