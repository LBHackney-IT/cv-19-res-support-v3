using AutoFixture;
using cv19ResSupportV3.Tests.V3.Helper;
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
    public class UpdateHelpRequestUseCaseTest
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private UpdateHelpRequestUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new UpdateHelpRequestUseCase(_mockGateway.Object);
        }

        [Test]
        public void ExecuteUpdatesRequestInDatabase()
        {
            var request = new UpdateHelpRequest()
            {
                Id = 7
            };
            var result = new HelpRequest()
            {
                Id = 7
            };
            _mockGateway.Setup(s => s.UpdateHelpRequest(It.IsAny<UpdateHelpRequest>())).Returns(result);
            var response = _classUnderTest.Execute(request);
            response.Should().BeEquivalentTo(request);
        }
    }
}
