using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.UseCases
{
    [TestFixture]
    public class DeleteCallHandlerUseCaseTests
    {
        private Mock<ICallHandlerGateway> _mockGateway;
        private DeleteCallHandlerUseCase _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _mockGateway = new Mock<ICallHandlerGateway>();
            _classUnderTest = new DeleteCallHandlerUseCase(_mockGateway.Object);
        }

        [Test]
        public void ExecuteMethodReturnsTrueIfIdExists()
        {
            // Arrange
            _mockGateway.Setup(gw => gw.DeleteCallHandler(It.IsAny<int>())).Returns(true);

            // Act
            var response = _classUnderTest.Execute(1);

            // Assert
            response.Should().BeTrue();
        }

        [Test]
        public void ExecuteMethodReturnsFalseIfIdNotExists()
        {
            // Arrange
            _mockGateway.Setup(gw => gw.DeleteCallHandler(It.IsAny<int>())).Returns(false);

            // Act
            var response = _classUnderTest.Execute(1);

            // Assert
            response.Should().BeFalse();
        }
    }
}
