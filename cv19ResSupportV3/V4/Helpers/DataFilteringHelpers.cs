using System;
using System.Linq;
using cv19ResSupportV3.V4.UseCase.Enumeration;

namespace cv19ResSupportV3.V4.Helpers
{
    public static class DataFilteringHelpers
    {
        public static string[] GetExcludedHelpTypes(string allowedHelpType)
            => HelpTypes.Excluded.Where(x => x != allowedHelpType).ToArray();
    }
}
