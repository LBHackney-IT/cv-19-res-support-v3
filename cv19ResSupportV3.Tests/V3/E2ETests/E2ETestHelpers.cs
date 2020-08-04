using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.Tests.V3.E2ETests
{
    public static class E2ETestHelpers
    {
        public static HelpRequestEntity AddResidentWithRelatedEntitiesToDb(HelpRequestsContext context)
        {
            var helpRequest = new Fixture().Create<HelpRequestEntity>();

            context.HelpRequestEntities.Add(helpRequest);
            context.SaveChanges();

            return helpRequest;
        }
    }
}
