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

        public static void ClearTable(HelpRequestsContext context)
        {
            var addedCases = context.CaseNoteEntities;
            context.CaseNoteEntities.RemoveRange(addedCases);
            var addedEntities = context.HelpRequestEntities;
            context.HelpRequestEntities.RemoveRange(addedEntities);
            var addedResidents = context.ResidentEntities;
            context.ResidentEntities.RemoveRange(addedResidents);
            var addedLookups = context.Lookups;
            context.Lookups.RemoveRange(addedLookups);
            context.SaveChanges();
        }
    }
}
