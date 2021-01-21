using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V3.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.UseCase
{
    [TestFixture]
    public class CreateHelpRequestCallUseCaseTests
    {
        private Mock<IHelpRequestCallGateway> _mockHelpRequestCallGateway;
        private CreateHelpRequestCallUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockHelpRequestCallGateway = new Mock<IHelpRequestCallGateway>();
            _classUnderTest = new CreateHelpRequestCallUseCase(_mockHelpRequestCallGateway.Object);
        }

        [Test]
        public void ItPersistsTheCallWhenTheRequestExists()
        {
            int requestId = 123;
            var command = new Fixture().Create<CreateHelpRequestCall>();
            _classUnderTest.Execute(requestId, command);
            _mockHelpRequestCallGateway.Verify(
                g => g.CreateHelpRequestCall(
                    requestId,
                    It.Is<CreateHelpRequestCall>(c =>
                        c.HelpRequestId == command.HelpRequestId
                    )
                ), Times.Once);
        }

        [Test]
        public void ItReturnsTheResultingCallId()
        {
            int requestId = 123;
            int resultingCallId = 1;
            _mockHelpRequestCallGateway
                .Setup(s => s.CreateHelpRequestCall(requestId, It.IsAny<CreateHelpRequestCall>()))
                .Returns(resultingCallId);
            var command = new Fixture().Create<CreateHelpRequestCall>();
            var response = _classUnderTest.Execute(requestId, command);
            response.Should().Be(resultingCallId);
        }

        [Test]
        public void ItReturnsNullWhenHelpRequestDoesNotExist()
        {
            int id = 1234;
            var dataToSave = new Fixture().Create<CreateHelpRequestCall>();
            var response = _classUnderTest.Execute(id, dataToSave);
            response.Should().Be(0);
        }
    }
}
