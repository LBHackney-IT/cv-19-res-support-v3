using System.Linq;
using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Requests;
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
    public class GetHelpRequestUseCaseTests
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private GetHelpRequestUseCase _classUnderTest;
        private Fixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new GetHelpRequestUseCase(_mockGateway.Object);
        }

        [Test]
        public void ReturnsASingleHelpRequest()
        {
            var stubbedRequest = _fixture.Create<HelpRequestEntity>();
            _mockGateway.Setup(x => x.GetHelpRequest(It.IsAny<int>())).Returns(stubbedRequest);
            var response = _classUnderTest.Execute(stubbedRequest.Id);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(stubbedRequest.ToResponse());
        }
    }
}
