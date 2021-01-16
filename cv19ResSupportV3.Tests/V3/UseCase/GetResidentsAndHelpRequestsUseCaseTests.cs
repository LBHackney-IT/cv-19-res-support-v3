using System.Collections.Generic;
using cv19ResSupportV3.Tests.V3.Helpers;
//using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V3.UseCase;
using FluentAssertions;
using FluentAssertions.Common;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.UseCase
{
    [TestFixture]
    public class GetResidentsAndHelpRequestsUseCaseTests
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private GetResidentsAndHelpRequestsUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new GetResidentsAndHelpRequestsUseCase(_mockGateway.Object);
        }

        [Test]
        public void ReturnsPopulatedHelpRequestListIfParamsProvided()
        {
            var reqParams = new SearchRequest() { Postcode = "test" };
            var stubbedRequests = new List<HelpRequestWithResident>() { new HelpRequestWithResident() { Id = 1, ResidentId = 2 }, new HelpRequestWithResident() { Id = 3, ResidentId = 2 } };
            _mockGateway.Setup(x => x.SearchHelpRequestsWithResidents(reqParams)).Returns(stubbedRequests);
            var response = _classUnderTest.Execute(reqParams);
            _mockGateway.Verify(x => x.SearchHelpRequestsWithResidents(reqParams), Times.Once());

            response.Should().NotBeNull();
        }

        [Test]
        public void ReturnsEmptyHelpRequestListIfNoParamsProvided()
        {
            var reqParams = new SearchRequest();
            var expectedResponse = new List<HelpRequestResponse>();
            var response = _classUnderTest.Execute(reqParams);
            _mockGateway.Verify(x => x.SearchHelpRequestsWithResidents(It.IsAny<SearchRequest>()), Times.Never);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
