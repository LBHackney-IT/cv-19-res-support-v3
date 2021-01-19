using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Gateways
{
    [TestFixture]
    public class CaseNotesGatewayTest : DatabaseTests
    {
        private CaseNotesGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new CaseNotesGateway(DatabaseContext);
        }
        [Test]
        public void PatchCaseNotePatchesTheFirstCaseNote()
        {
            var resident = EntityHelpers.createResident(114);
            var helpRequest = EntityHelpers.createHelpRequestEntity(43, resident.Id);

            var caseNote = new CaseNoteEntity() { CaseNote = "before update", ResidentId = resident.Id, HelpRequestId = helpRequest.Id };

            DatabaseContext.ResidentEntities.Add(resident);
            DatabaseContext.HelpRequestEntities.Add(helpRequest);
            var entity = DatabaseContext.CaseNoteEntities.Add(caseNote);
            DatabaseContext.SaveChanges();

            _classUnderTest.PatchCaseNote(helpRequest.Id, resident.Id, "after update");

            var oldEntity = DatabaseContext.CaseNoteEntities.Find(caseNote.Id);
            DatabaseContext.Entry(oldEntity).State = EntityState.Detached;
            var updatedEntity = DatabaseContext.CaseNoteEntities.Find(caseNote.Id);

            updatedEntity.CaseNote.Should().Be("after update");
        }

    }
}
