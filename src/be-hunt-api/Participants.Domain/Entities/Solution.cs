using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Participants.Domain.Entities
{
    public sealed class Solution
    {
        public int SolutionType { get; set; }
        public required string Data { get; set; }
    }
}
