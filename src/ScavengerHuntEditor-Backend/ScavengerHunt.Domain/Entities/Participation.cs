using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerHunt.Domain.Entities
{
    public sealed class Participation
    {
        public int Id { get; set; }

        public int ParticipantId { get; set; }
        public required Participant Participant { get; set; }

        public int HuntId { get; set; }
        public required Hunt Hunt { get; set; }

        public int CurrentAssignmentId { get; set; }
        public required Assignment CurrentAssignment { get; set; }
    }
}
