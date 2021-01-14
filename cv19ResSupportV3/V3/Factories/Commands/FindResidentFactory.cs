using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.Factories.Commands
{
    public static class FindResidentFactory
    {
        public static FindResident ToFindResidentCommand(this CreateResidentAndHelpRequest helpRequest)
        {
            return new FindResident()
            {
                Uprn = helpRequest.Uprn,
                FirstName = helpRequest.FirstName,
                LastName = helpRequest.LastName,
                DobMonth = helpRequest.DobMonth,
                DobYear = helpRequest.DobYear,
                DobDay = helpRequest.DobDay,
            };
        }
    }
}
