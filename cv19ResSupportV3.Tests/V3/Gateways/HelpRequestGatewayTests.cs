using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Gateways
{
    [TestFixture]
    public class HelpRequestGatewayTests : DatabaseTests
    {
        private readonly Fixture _fixture = new Fixture();
        private HelpRequestGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new HelpRequestGateway(DatabaseContext);
        }

        [Test]
        public void CreateHelpRequestReturnsTheRequestIfCreated()
        {
            var helpRequest = _fixture.Create<HelpRequestEntity>();
            var response = _classUnderTest.CreateHelpRequest(helpRequest);
            response.Should().Be(helpRequest.Id);
        }

        [Test]
        public void CreateDuplicateHelpRequestTheLatestAsMaster()
        {
            var helpRequest = _fixture.Create<HelpRequest>();
            helpRequest.Id = 0;
            var response1 = _classUnderTest.CreateHelpRequest(helpRequest.ToEntity());
            var response2 = _classUnderTest.CreateHelpRequest(helpRequest.ToEntity());
            var firstRecordToCheck = DatabaseContext.HelpRequestEntities.Find(response1);
            var secondRecordToCheck = DatabaseContext.HelpRequestEntities.Find(response2);
            firstRecordToCheck.RecordStatus.Should().Be("DUPLICATE");
            secondRecordToCheck.RecordStatus.Should().Be("MASTER");
        }
    }
}
