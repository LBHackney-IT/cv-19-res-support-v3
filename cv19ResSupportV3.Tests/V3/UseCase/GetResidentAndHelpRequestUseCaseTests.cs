using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.UseCase
{
    [TestFixture]
    public class GetResidentAndHelpRequestUseCaseTests
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private GetResidentAndHelpRequestUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new GetResidentAndHelpRequestUseCase(_mockGateway.Object);
        }

        [Test]
        public void ReturnsASingleHelpRequest()
        {
            var stubbedResident = EntityHelpers.createResident(21);
            var stubbedRequest = EntityHelpers.createHelpRequestEntity(123, 21);
            var stubbedRequestWithResident = stubbedResident.ToDomain(stubbedRequest);
            _mockGateway.Setup(x => x.GetHelpRequest(It.IsAny<int>())).Returns(stubbedRequestWithResident);
            var response = _classUnderTest.Execute(stubbedRequest.Id);
            _mockGateway.Verify(x => x.GetHelpRequest(It.Is<int>(x => x == 123)), Times.Once());

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
            var stubbedRequestWithResident = stubbedResident.ToDomain(stubbedRequest);
            _mockGateway.Setup(x => x.GetHelpRequest(It.IsAny<int>())).Returns(stubbedRequestWithResident);
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
