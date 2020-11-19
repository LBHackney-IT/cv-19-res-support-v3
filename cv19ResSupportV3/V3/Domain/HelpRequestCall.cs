using System;

namespace cv19ResSupportV3.V3.Domain
{
    public class HelpRequestCall
    {
        public int Id { get; set; }
        public bool? helpRequestId { get; set; }
        public bool? callType { get; set; }
        public string callOutcome { get; set; }
        public string callDateTime { get; set; }
    }
}
