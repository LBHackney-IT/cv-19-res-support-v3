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
        public void ExecuteWithValidIdSavesRequestToDatabase()
        {
            int id = 1;
            _mockHelpRequestCallGateway.Setup(s => s.CreateHelpRequestCall(id, It.IsAny<HelpRequestCallEntity>())).Returns(id);
            var dataToSave = new Fixture().Build<CreateHelpRequestCall>().Create();
            var response = _classUnderTest.Execute(id, dataToSave);
            response.Should().Be(id);
        }

        [Test]
        public void ExecuteWithInvalidIdReturnsNull()
        {
            int id = 1;
            var dataToSave = new Fixture().Build<CreateHelpRequestCall>().Create();
            var response = _classUnderTest.Execute(id, dataToSave);
            response.Should().Be(0);
        }
    }
}
