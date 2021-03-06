using AutoFixture;
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
    public class CreateResidentUseCaseTests
    {
        private Mock<IResidentGateway> _mockGateway;
        private CreateResidentsUseCase _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _mockGateway = new Mock<IResidentGateway>();
            _classUnderTest = new CreateResidentsUseCase(_mockGateway.Object);
        }

        [Test]
        public void ExecuteMethodCallsResidentGateway()
        {
            var request = new Fixture().Build<ResidentRequestBoundary>().Create();
            _mockGateway.Setup(gw => gw.CreateResident(It.IsAny<CreateResident>())).Returns(request.ToResident());
            var response = _classUnderTest.Execute(request);
            _mockGateway.Verify(uc => uc.CreateResident(It.IsAny<CreateResident>()), Times.Once);
            var expectedResponse = request.ToResident().ToResponse();
            response.Should().BeEquivalentTo(expectedResponse);
        }

    }
}
