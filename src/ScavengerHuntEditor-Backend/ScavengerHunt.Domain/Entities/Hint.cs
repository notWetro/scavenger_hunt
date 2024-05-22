using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerHunt.Domain.Entities
{
    public sealed class Hint
    {
        public int Id { get; set; }
        public HintType HintType { get; set; }
        public string Data { get; set; } = default!;

        public int AssignmentId { get; set; }
        public required Assignment Assignment { get; set; }
    }
}
