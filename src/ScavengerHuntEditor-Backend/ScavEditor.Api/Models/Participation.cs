using ScavEditor.Api.Data;

namespace ScavEditor.Api.Models
{
    public sealed class Participation
    {
        public int Id { get; set; }
        public ProgressionStatus ProgressionStatus { get; set; }

        public int IdParticipant { get; set; }
        public Participant? Participant { get; set; }
        
        public int IdScavengerHunt { get; set; }
        public ScavengerHunt? ScavengerHunt { get; set; }

        public int IdCurrentTask { get; set; }
        public TaskBase? CurrentTask { get; set; }
    }
}
