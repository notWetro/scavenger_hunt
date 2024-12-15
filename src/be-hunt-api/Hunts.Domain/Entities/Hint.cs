namespace Hunts.Domain.Entities
{
    public sealed class Hint
    {
        public int Id { get; set; }
        public HintType HintType { get; set; }
        public string Data { get; set; } = default!;    
        public string? additionalData {get; set;}
        public int AssignmentId { get; set; }
        public required Assignment Assignment { get; set; }
    }
}
