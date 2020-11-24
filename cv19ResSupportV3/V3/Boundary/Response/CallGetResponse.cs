using System;

namespace cv19ResSupportV3.V3.Boundary.Response
{
    public class CallGetResponse
    {
        public int Id { get; set; }
        public int HelpRequestId { get; set; }
        public string CallType { get; set; }
        public string CallOutcome { get; set; }
        public DateTime CallDateTime { get; set; }
    }
}
