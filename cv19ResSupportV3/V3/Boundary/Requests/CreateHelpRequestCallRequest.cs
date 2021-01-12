using System;

namespace cv19ResSupportV3.V3.Boundary.Requests
{
    public class CreateHelpRequestCallRequest
    {
        public int HelpRequestId { get; set; }
        public string CallType { get; set; }
        public string CallDirection { get; set; }
        public string CallOutcome { get; set; }
        public DateTime CallDateTime { get; set; }

        public string CallHandler { get; set; }
    }
}
