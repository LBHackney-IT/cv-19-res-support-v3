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
            var stubbedResident = EntityHelpers.createResident(21);
            var stubbedRequest = EntityHelpers.createHelpRequestEntity(123, 21);
            _mockGateway.Setup(x => x.GetHelpRequest(It.IsAny<int>())).Returns(stubbedRequest.ToDomain());
            _mockGateway.Setup(x => x.GetResident(It.IsAny<int>())).Returns(stubbedResident.ToDomain());
            var response = _classUnderTest.Execute(stubbedRequest.Id);
            _mockGateway.Verify(x => x.GetHelpRequest(It.Is<int>(x => x == 123)), Times.Once());
            _mockGateway.Verify(x => x.GetResident(It.Is<int>(x => x == stubbedResident.Id)), Times.Once());

            response.Should().NotBeNull();
            response.ResidentId.Should().Be(stubbedResident.Id);
            response.Id.Should().Be(stubbedRequest.Id);
        }

        [Test]
        public void ReturnsASingleHelpRequestWithCalls()
        {
            var stubbedResident = EntityHelpers.createResident(22);
            var stubbedRequest = EntityHelpers.createHelpRequestEntity(5, 22);
            var calls = EntityHelpers.createHelpRequestCallEntities();
            calls.ForEach(x => x.HelpRequestId = 5);
            stubbedRequest.HelpRequestCalls = calls;
            _mockGateway.Setup(x => x.GetHelpRequest(It.IsAny<int>())).Returns(stubbedRequest.ToDomain());
            _mockGateway.Setup(x => x.GetResident(It.IsAny<int>())).Returns(stubbedResident.ToDomain());
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
