using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
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

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new GetHelpRequestUseCase(_mockGateway.Object);
        }

        [Test]
        public void ReturnsASingleHelpRequest()
        {
            var stubbedRequest = EntityHelpers.createHelpRequestEntity();
            _mockGateway.Setup(x => x.GetHelpRequest(It.IsAny<int>())).Returns(stubbedRequest);
            var response = _classUnderTest.Execute(stubbedRequest.Id);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(stubbedRequest.ToResponse());
        }

        [Test]
        public void ReturnsASingleHelpRequestWithCalls()
        {
            var stubbedRequest = EntityHelpers.createHelpRequestEntity(5);
            var calls = EntityHelpers.createHelpRequestCallEntities();
            calls.ForEach(x => x.HelpRequestId = 5);
            stubbedRequest.HelpRequestCalls = calls;
            _mockGateway.Setup(x => x.GetHelpRequest(It.IsAny<int>())).Returns(stubbedRequest);
            var response = _classUnderTest.Execute(stubbedRequest.Id);
            response.Should().NotBeNull();
            response.HelpRequestCalls.Should().BeEquivalentTo(calls, options =>
            {
                options.Excluding(ex => ex.HelpRequestEntity);
                return options;
            });
        }
    }
}
