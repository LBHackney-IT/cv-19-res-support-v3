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
    public class GetLookupsUseCaseTests
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private GetLookupsUseCase _classUnderTest;
        private Fixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new GetLookupsUseCase(_mockGateway.Object);
        }

        [Test]
        public void ReturnsPopulatedLookupsParamsProvided()
        {
            var reqParams = new LookupQueryParams() {LookupGroup = "help_needed"};
            var stubbedLookups = _fixture.CreateMany<LookupEntity>();
            foreach (var req in stubbedLookups)
            {
                req.LookupGroup = "help_needed";
            }
            _mockGateway.Setup(x => x.GetLookups(reqParams)).Returns(stubbedLookups.ToList());
            var response = _classUnderTest.Execute(reqParams);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(stubbedLookups.ToResponse());
        }

        [Test]
        public void ReturnsAllCallbacksIfNoParamsProvided()
        {
            var reqParams = new LookupQueryParams();
            var stubbedRequests = _fixture.CreateMany<LookupEntity>();
            var expectedResponse = stubbedRequests.ToResponse();
            _mockGateway.Setup(x => x.GetLookups(reqParams)).Returns(stubbedRequests.ToList());
            var response = _classUnderTest.Execute(reqParams);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
