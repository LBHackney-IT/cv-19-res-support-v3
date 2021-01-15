//using cv19ResSupportV3.Tests.V3.Helpers;

using System.Linq;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain.Commands;
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
            var residentId = 501;
            var reqParams = new CallbackQuery() { HelpNeeded = "shielding" };
            var stubbedResident = EntityHelpers.createResident(residentId);
            var stubbedRequests = EntityHelpers.createHelpRequestEntities(3);
            foreach (var req in stubbedRequests)
            {
                req.ResidentId = residentId;
                req.HelpNeeded = "Shielding";
            }

            var expectedResponse = stubbedRequests.Select(request => stubbedResident.ToDomain(request)).ToList();
            _mockGateway.Setup(x => x.GetCallbacks(reqParams)).Returns(stubbedRequests.ToDomain());
            _mockGateway.Setup(x => x.GetResident(residentId)).Returns(stubbedResident.ToDomain());
            var response = _classUnderTest.Execute(reqParams);
            _mockGateway.Verify(x => x.GetCallbacks(reqParams), Times.Once);
            _mockGateway.Verify(x => x.GetResident(residentId), Times.Exactly(3));
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResponse);
        }

        //        [Test]
        //        public void ReturnsAllCallbacksIfNoParamsProvided()
        //        {
        //            var reqParams = new CallbackQuery();
        //            var stubbedRequests = EntityHelpers.createHelpRequestEntities();
        //            var expectedResponse = stubbedRequests.ToDomain();
        //            _mockGateway.Setup(x => x.GetCallbacks(reqParams)).Returns(stubbedRequests.ToDomain());
        //            var response = _classUnderTest.Execute(reqParams);
        //            response.Should().NotBeNull();
        //            response.Should().BeEquivalentTo(expectedResponse);
        //        }
    }
}
