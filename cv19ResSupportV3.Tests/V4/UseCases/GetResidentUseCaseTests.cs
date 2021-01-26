using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.UseCases
{
    [TestFixture]
    public class GetResidentUseCaseTests
    {
        private Mock<IResidentGateway> _mockGateway;
        private GetResidentsUseCase _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _mockGateway = new Mock<IResidentGateway>();
            _classUnderTest = new GetResidentsUseCase(_mockGateway.Object);
        }

        [Test]
        public void ExecuteMethodCallsResidentGateway()
        {
            var gatewayResponse = new Fixture().Build<Resident>().Create();
            _mockGateway.Setup(gw => gw.GetResident(It.IsAny<int>())).Returns(gatewayResponse);
            var response = _classUnderTest.Execute(gatewayResponse.Id);
            _mockGateway.Verify(uc => uc.GetResident(It.IsAny<int>()), Times.Once);
            response.Should().BeEquivalentTo(gatewayResponse.ToResponse());
        }

    }
}
