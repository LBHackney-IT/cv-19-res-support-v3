using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V4.Helpers;

namespace cv19ResSupportV3.V3.Factories.Commands
{
    public static class CallbackFactory
    {
        public static CallbackQuery ToCommand(this CallbackRequestParams callback)
        {
            return new CallbackQuery()
            {
                Master = callback.Master,
                HelpNeeded = callback.HelpNeeded,
                ExcludedHelpTypes = DataFilteringHelpers.GetExcludedHelpTypes(callback.IncludeType),
            };
        }
    }
}
