using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.UseCase
{
    [TestFixture]
    public class GetCallbacksUseCaseTests
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private GetCallbacksUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new GetCallbacksUseCase(_mockGateway.Object);
        }

        [Test]
        public void ReturnsPopulatedHelpRequestListIfParamsProvided()
        {
            var reqParams = new CallbackRequestParams() { Master = "true" };
            var stubbedRequests = EntityHelpers.createHelpRequestEntities();
            foreach (var req in stubbedRequests)
            {
                req.RecordStatus = "MASTER";
            }
            _mockGateway.Setup(x => x.GetCallbacks(reqParams)).Returns(stubbedRequests.ToDomain());
            var response = _classUnderTest.Execute(reqParams);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(stubbedRequests.ToResponse());
        }

        [Test]
        public void ReturnsAllCallbacksIfNoParamsProvided()
        {
            var reqParams = new CallbackRequestParams();
            var stubbedRequests = EntityHelpers.createHelpRequestEntities();
            var expectedResponse = stubbedRequests.ToResponse();
            _mockGateway.Setup(x => x.GetCallbacks(reqParams)).Returns(stubbedRequests.ToDomain());
            var response = _classUnderTest.Execute(reqParams);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
