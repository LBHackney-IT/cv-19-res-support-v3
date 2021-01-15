using System;

namespace cv19ResSupportV3.V3.Domain
{
    public class ResidentCaseNote
    {
        public int Id { get; set; }
        public int ResidentId { get; set; }
        public int HelpRequestId { get; set; }
        public string CaseNote { get; set; }
    }
}
