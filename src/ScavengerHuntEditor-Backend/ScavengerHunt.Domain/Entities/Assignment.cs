using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScavengerHunt.Domain.Entities
{
    public sealed class Assignment
    {
        public int Id { get; set; }

        public int HuntId { get; set; }
        public Hunt Hunt { get; set; }

        public int HintId { get; set; }
        public Hint Hint { get; set; }

        public int SolutionId { get; set; }
        public Solution Solution { get; set; }

    }
}
