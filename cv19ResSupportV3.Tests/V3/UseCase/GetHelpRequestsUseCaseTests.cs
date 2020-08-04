using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V3.UseCase;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.UseCase
{
    [TestFixture]
    public class GetHelpRequestsUseCaseTests
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private GetHelpRequestsUseCase _classUnderTest;
        private Fixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new GetHelpRequestsUseCase(_mockGateway.Object);
        }

        [Test]
        public void ReturnsPopulatedHelpRequestListIfParamsProvided()
        {
            var reqParams = new RequestQueryParams {Postcode = "test"};
            var stubbedRequests = _fixture.CreateMany<HelpRequestEntity>();
            _mockGateway.Setup(x => x.SearchHelpRequests(reqParams)).Returns(stubbedRequests.ToList());
            var response = _classUnderTest.Execute(reqParams);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(stubbedRequests.ToResponse());
        }

        [Test]
        public void ReturnsEmptyHelpRequestListIfNoParamsProvided()
        {
            var reqParams = new RequestQueryParams();
            var stubbedRequests = _fixture.CreateMany<HelpRequestEntity>();
            var expectedResponse = new List<HelpRequestEntity>();
            _mockGateway.Setup(x => x.SearchHelpRequests(reqParams)).Returns(stubbedRequests.ToList());
            var response = _classUnderTest.Execute(reqParams);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
