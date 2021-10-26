using System.Collections.Generic;
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
    public class GetCallHandlersUseCaseTests
    {
        private Mock<ICallHandlerGateway> _mockGateway;
        private GetCallHandlersUseCase _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _mockGateway = new Mock<ICallHandlerGateway>();
            _classUnderTest = new GetCallHandlersUseCase(_mockGateway.Object);
        }

        [Test]
        public void ExecuteMethodCallsCallHandlerGateway()
        {
            var gatewayResponse = new Fixture().Build<List<CallHandlerResponse>>().Create();
            _mockGateway.Setup(gw => gw.GetCallHandlers()).Returns(gatewayResponse);
            var response = _classUnderTest.Execute();
            _mockGateway.Verify(uc => uc.GetCallHandlers(), Times.Once);
            response.Should().BeEquivalentTo(gatewayResponse.ToResponse());
        }

    }
}
