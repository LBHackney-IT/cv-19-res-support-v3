using System.Collections.Generic;

namespace cv19ResSupportV3.V3.Domain.Commands
{
    public class CallbackQuery
    {
        public string Master { get; set; }
        public string HelpNeeded { get; set; }
        public IEnumerable<string> ExcludedHelpTypes { get; set; }
    }
}
