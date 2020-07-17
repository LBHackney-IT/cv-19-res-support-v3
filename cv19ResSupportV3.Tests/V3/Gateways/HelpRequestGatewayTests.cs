using AutoFixture;
using cv19ResRupportV3.V3.Gateways;
using cv19ResRupportV3.V3.Infrastructure;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResRupportV3.Tests.V3.Gateways
{
    [TestFixture]
    public class ExampleGatewayTests : DatabaseTests
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
    }
}
