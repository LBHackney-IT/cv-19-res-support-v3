using AutoFixture;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase;
using cv19ResSupportV3.V4.Factories;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.UseCases
{
    [TestFixture]
    public class UpsertCallHandlerUseCaseTests
    {
        private Mock<ICallHandlerGateway> _mockGateway;
        private UpsertCallHandlerUseCase _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _mockGateway = new Mock<ICallHandlerGateway>();
            _classUnderTest = new UpsertCallHandlerUseCase(_mockGateway.Object);
        }

        [Test]
        public void ExecuteMethodCallsUpdateIfIdProvided()
        {
            // Arrange
            var request = new Fixture().Build<CallHandlerCommand>().Create();
            request.Id = 1;
            var gatewayResponse = request.ToEntity().ToDomain();
            _mockGateway.Setup(gw => gw.UpdateCallHandler(It.IsAny<CallHandlerCommand>())).Returns(gatewayResponse);

            // Act
            var response = _classUnderTest.Execute(request);

            // Assert
            _mockGateway.Verify(uc => uc.CreateCallHandler(It.IsAny<CallHandlerCommand>()), Times.Never);
            _mockGateway.Verify(uc => uc.UpdateCallHandler(It.IsAny<CallHandlerCommand>()), Times.Once);
            response.Should().BeEquivalentTo(gatewayResponse);
        }

        [Test]
        public void ExecuteMethodCallsCreateIfIdNotProvided()
        {
            // Arrange
            var request = new Fixture().Build<CallHandlerCommand>().Create();
            request.Id = null;
            var gatewayResponse = request.ToEntity().ToDomain();
            _mockGateway.Setup(gw => gw.CreateCallHandler(It.IsAny<CallHandlerCommand>())).Returns(gatewayResponse);

            // Act
            var response = _classUnderTest.Execute(request);

            // Assert
            _mockGateway.Verify(uc => uc.CreateCallHandler(It.IsAny<CallHandlerCommand>()), Times.Once);
            _mockGateway.Verify(uc => uc.UpdateCallHandler(It.IsAny<CallHandlerCommand>()), Times.Never);
            response.Should().BeEquivalentTo(gatewayResponse);
        }
    }
}
