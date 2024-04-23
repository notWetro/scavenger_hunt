using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerHunt.Domain.Entities
{
    public sealed class Solution
    {
        public int Id { get; set; }
        public SolutionType Type { get; set; }
        public string Data { get; set; } = string.Empty;

        public int AssignmentId { get; set; }
        public Assignment Assignment { get; set; }
    }
}
