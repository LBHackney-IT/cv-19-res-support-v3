using System;
using System.Collections.Generic;

namespace cv19ResSupportV3.V3.Boundary.Requests
{
    public class UpdateStaffAssignmentsRequestBoundary
    {
        public string HelpNeeded { get; set; }
        public List<string> StaffMembers { get; set; }
    }
}
