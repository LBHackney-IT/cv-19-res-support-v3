using System.Linq;
using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4.Boundary.Response;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.UseCase;
using cv19ResSupportV3.V4.UseCase.Enumeration;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.UseCases
{
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Justifiable for unit tests to separate between Method, Condition, Result.")]
    public class GetResidentHelpRequestUseCaseTests
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private GetResidentHelpRequestUseCase _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new GetResidentHelpRequestUseCase(_mockGateway.Object);
        }

        [Test]
        public void ExecuteMethodCallsHelpRequestGateway()
        {
            var gatewayResponse = new Fixture().Build<HelpRequestWithResident>().Create();

            _mockGateway.Setup(gw => gw.GetHelpRequest(It.IsAny<int>())).Returns(gatewayResponse);

            var response = _classUnderTest.Execute(1, 2, HelpTypes.Excluded);

            _mockGateway.Verify(uc => uc.GetHelpRequest(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void Execute_HelpTypeExcluded_ReturnsEmpty()
        {
            int residentId = 2;
            var gatewayResponse = new HelpRequestWithResident()
            {
                HelpNeeded = HelpTypes.Excluded.First(),
                ResidentId = residentId
            };

            _mockGateway.Setup(gw => gw.GetHelpRequest(It.IsAny<int>())).Returns(gatewayResponse);

            var response = _classUnderTest.Execute(residentId, 3, HelpTypes.Excluded);

            response.Should().BeEquivalentTo(new ResidentHelpRequestResponse());
        }

        [Test]
        public void Execute_HelpTypeNotExcluded_ReturnsResponse()
        {
            int residentId = 2;
            var gatewayResponse = new HelpRequestWithResident() { HelpNeeded = "Contact Tracing", ResidentId = residentId };

            _mockGateway.Setup(gw => gw.GetHelpRequest(It.IsAny<int>())).Returns(gatewayResponse);

            var response = _classUnderTest.Execute(residentId, 3, HelpTypes.Excluded);

            response.Should().BeEquivalentTo(gatewayResponse.ToResidentHelpRequestResponse());
        }

        [Test]
        public void Execute_ResidentDoesNotMatchHelpRequest_ReturnsEmpty()
        {
            var gatewayResponse = new HelpRequestWithResident() { HelpNeeded = "Contact Tracing", ResidentId = 1 };

            _mockGateway.Setup(gw => gw.GetHelpRequest(It.IsAny<int>())).Returns(gatewayResponse);

            var response = _classUnderTest.Execute(2, 3, HelpTypes.Excluded);

            response.Should().BeEquivalentTo(new ResidentHelpRequestResponse());
        }
    }
}