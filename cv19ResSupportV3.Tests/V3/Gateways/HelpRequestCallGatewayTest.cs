using System.Linq;
using AutoFixture;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
using FluentAssertions;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.Gateways
{
    [TestFixture]
    public class HelpRequestCallGatewayTests : DatabaseTests
    {
        private HelpRequestCallGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new HelpRequestCallGateway(DatabaseContext);
        }

        [Test]
        public void CreateHelpRequestCallReturnsTheRequestIfCreated()
        {
            DatabaseContext.HelpRequestEntities.Add(EntityHelpers.createHelpRequestEntity(5));
            DatabaseContext.SaveChanges();
            var helpRequestEntity = DatabaseContext.HelpRequestEntities.First();
            var command = new Fixture().Create<CreateHelpRequestCall>();
            command.HelpRequestId = helpRequestEntity.Id;
            var response = _classUnderTest.CreateHelpRequestCall(helpRequestEntity.Id, command);
            var persistedCall = DatabaseContext.HelpRequestCallEntities.OrderByDescending(x => x.Id).First();
            response.Should().Be(persistedCall.Id);
        }

    }
}
