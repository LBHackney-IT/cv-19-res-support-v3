using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.Factories.Commands
{
    public static class FindResidentFactory
    {
        public static FindResident ToFindResidentCommand(this CreateResident command)
        {
            return new FindResident()
            {
                Uprn = command.Uprn,
                FirstName = command.FirstName,
                LastName = command.LastName,
                DobMonth = command.DobMonth,
                DobYear = command.DobYear,
                DobDay = command.DobDay,
                EmailAddress = command.EmailAddress,
                Postcode = command.Postcode,
                NhsNumber = command.NhsNumber,
                NhsCtasId = command.NhsCtasId
            };
        }
    }
}
