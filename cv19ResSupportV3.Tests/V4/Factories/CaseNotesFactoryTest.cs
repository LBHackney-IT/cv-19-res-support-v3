using System.Linq;
using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V4.Boundary.Responses;
using cv19ResSupportV3.V4.Factories;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.Factories
{
    [TestFixture]
    public class CaseNotesFactoryTest
    {
        Fixture _fixture = new Fixture();

        [Test]
        public void CanMapADomainToAResponseObject()
        {
            var domain = _fixture.Build<ResidentCaseNote>().Create();
            var response = domain.ToResponse();

            response.Should().BeEquivalentTo(new CaseNoteResponse() {
                CaseNote = domain.CaseNote,
                HelpRequestId = domain.HelpRequestId,
                Id = domain.Id,
                ResidentId = domain.ResidentId
            });
        }
    }
}
