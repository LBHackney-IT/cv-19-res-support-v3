using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Gateways
{
    [TestFixture]
    public class HelpRequestCallGatewayTests : DatabaseTests
    {
        private readonly Fixture _fixture = new Fixture();
        private HelpRequestCallGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
//            _classUnderTest = new HelpRequestCallGateway(DatabaseContext);
            _classUnderTest = new HelpRequestCallGateway();
        }

        [Test]
        public void CreateHelpRequestCallReturnsTheRequestIfCreated()
        {
            var helpRequestCall = _fixture.Create<HelpRequestCallEntity>();
            var response = _classUnderTest.CreateHelpRequestCall(helpRequestCall);
            response.Should().Be(helpRequestCall.Id);
        }

    }
}
