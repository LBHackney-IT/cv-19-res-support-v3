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
    public class CreateHelpRequestUseCaseTests
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private CreateHelpRequestUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new CreateHelpRequestUseCase(_mockGateway.Object);
        }

        [Test]
        public void ExecuteSavesRequestToDatabase()
        {
            _mockGateway.Setup(s => s.CreateHelpRequest(It.IsAny<int>(), It.IsAny<CreateHelpRequest>())).Returns(1);
            var dataToSave = new Fixture().Build<CreateHelpRequest>().Create();
            var response = _classUnderTest.Execute(1, dataToSave);
            _mockGateway.Verify(m => m.CreateHelpRequest(It.IsAny<int>(), It.IsAny<CreateHelpRequest>()), Times.Once());
            response.Should().Be(1);
        }
    }
}
