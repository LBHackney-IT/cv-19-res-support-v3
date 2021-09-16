using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.UseCase;
using cv19ResSupportV3.V4.UseCase.Enumeration;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.UseCases
{
    [TestFixture]
    public class GetResidentHelpRequestsUseCaseTests
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private GetResidentHelpRequestsUseCase _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new GetResidentHelpRequestsUseCase(_mockGateway.Object);
        }

        [Test]
        public void ExecuteMethodCallsHelpRequestGateway()
        {
            var gatewayResponse = new Fixture().Build<List<HelpRequestWithResident>>().Create();

            _mockGateway.Setup(gw => gw.GetResidentHelpRequests(It.IsAny<int>())).Returns(gatewayResponse);
            var response = _classUnderTest.Execute(1);
            _mockGateway.Verify(uc => uc.GetResidentHelpRequests(It.IsAny<int>()), Times.Once);
            response.Should().BeEquivalentTo(gatewayResponse.ToResidentHelpRequestResponse());
        }

        [Test]
        public void UseCaseExcludesPredefinedExcludedHelpTypes()
        {
            var gatewayResponse = new List<HelpRequestWithResident>() {
                new HelpRequestWithResident() { HelpNeeded = HelpTypes.Excluded.First() },
                new HelpRequestWithResident() { HelpNeeded = "Contact Tracing" },
            };

            _mockGateway.Setup(gw => gw.GetResidentHelpRequests(It.IsAny<int>())).Returns(gatewayResponse);

            var response = _classUnderTest.Execute(1);

            response.Should().BeEquivalentTo(gatewayResponse.ToResidentHelpRequestResponse().Where(x => x.HelpNeeded != HelpTypes.Excluded.First()));
        }
    }
}
