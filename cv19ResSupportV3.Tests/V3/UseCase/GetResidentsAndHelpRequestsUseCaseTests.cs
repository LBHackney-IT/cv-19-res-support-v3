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
            var stubbedRequests = new List<HelpRequest>() { new HelpRequest() { Id = 1, ResidentId = 2 }, new HelpRequest() { Id = 3, ResidentId = 2 } };
            var stubbedResident = new Resident() { Id = 2, FirstName = "Tom" };
            _mockGateway.Setup(x => x.SearchHelpRequests(reqParams)).Returns(stubbedRequests);
            _mockGateway.Setup(x => x.GetResident(2)).Returns(stubbedResident);
            var response = _classUnderTest.Execute(reqParams);
            _mockGateway.Verify(x => x.SearchHelpRequests(reqParams), Times.Once());
            _mockGateway.Verify(x => x.GetResident(2), Times.Exactly(2));

            response.Should().NotBeNull();
            response[0].Id.Should().Be(1);
            response[0].ResidentId.Should().Be(2);
            response[0].FirstName.Should().Be("Tom");
            response[1].Id.Should().Be(3);
            response[1].ResidentId.Should().Be(2);
            response[1].FirstName.Should().Be("Tom");
            response.Count.Should().Be(2);
        }

        [Test]
        public void ReturnsEmptyHelpRequestListIfNoParamsProvided()
        {
            var reqParams = new SearchRequest();
            var expectedResponse = new List<HelpRequestResponse>();
            var response = _classUnderTest.Execute(reqParams);
            _mockGateway.Verify(x => x.SearchHelpRequests(It.IsAny<SearchRequest>()), Times.Never);
            _mockGateway.Verify(x => x.GetResident(It.IsAny<int>()), Times.Never);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
