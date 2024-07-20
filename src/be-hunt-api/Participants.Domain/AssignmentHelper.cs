using Participants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Participants.Domain
{
    public static class AssignmentHelper
    {
        public static bool CheckAssignmentData(Assignment assignment, string givenData)
        {
            if (assignment is not null && assignment.Solution.Data == givenData)
                return true;
            return false;
        }
    }
}
