using System.Linq;
using cv19ResSupportV3.Tests.V3.Helpers;
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
            var helpRequestCall = EntityHelpers.createHelpRequestCallEntity();
            helpRequestCall.HelpRequestId = helpRequestEntity.Id;
            var response = _classUnderTest.CreateHelpRequestCall(helpRequestEntity.Id, helpRequestCall);
            response.Should().Be(helpRequestCall.Id);
        }

    }
}
