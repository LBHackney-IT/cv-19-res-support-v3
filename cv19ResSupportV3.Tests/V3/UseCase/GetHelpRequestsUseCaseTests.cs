using System.Collections.Generic;
//using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Requests;
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
    public class GetHelpRequestsUseCaseTests
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private GetHelpRequestsUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new GetHelpRequestsUseCase(_mockGateway.Object);
        }
//
//        [Test]
//        public void ReturnsPopulatedHelpRequestListIfParamsProvided()
//        {
//            var reqParams = new SearchRequest() { Postcode = "test" };
//            var stubbedRequests = EntityHelpers.createHelpRequestEntities();
//            _mockGateway.Setup(x => x.SearchHelpRequests(reqParams)).Returns(stubbedRequests.ToDomain());
//            var response = _classUnderTest.Execute(reqParams);
//            response.Should().NotBeNull();
//            response.Should().BeEquivalentTo(stubbedRequests.ToResponse());
//        }
//
//        [Test]
//        public void ReturnsEmptyHelpRequestListIfNoParamsProvided()
//        {
//            var reqParams = new SearchRequest();
//            var stubbedRequests = EntityHelpers.createHelpRequestEntities();
//            var expectedResponse = new List<HelpRequestEntityOld>();
//            _mockGateway.Setup(x => x.SearchHelpRequests(reqParams)).Returns(stubbedRequests.ToDomain());
//            var response = _classUnderTest.Execute(reqParams);
//            response.Should().NotBeNull();
//            response.Should().BeEquivalentTo(expectedResponse);
//        }
    }
}
