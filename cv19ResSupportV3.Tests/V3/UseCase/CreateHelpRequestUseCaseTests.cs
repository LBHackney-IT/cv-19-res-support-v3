using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
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
            var expectedResponse = new HelpRequestCreateResponse
            {
                Id = 1
            };
            _mockGateway.Setup(s => s.CreateHelpRequest(It.IsAny<HelpRequestEntity>())).Returns(1);
            var dataToSave = new Fixture().Build<HelpRequest>().Create();
            var response = _classUnderTest.Execute(dataToSave);
            response.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
