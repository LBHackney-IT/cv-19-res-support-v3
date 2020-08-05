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
    public class GetCallbacksUseCaseTests
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private GetCallbacksUseCase _classUnderTest;
        private Fixture _fixture = new Fixture();

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new GetCallbacksUseCase(_mockGateway.Object);
        }

        [Test]
        public void ReturnsPopulatedHelpRequestListIfParamsProvided()
        {
            var reqParams = new CallbackRequestParams() {Master = "true"};
            var stubbedRequests = _fixture.CreateMany<HelpRequestEntity>();
            foreach (var req in stubbedRequests)
            {
                req.RecordStatus = "MASTER";
            }
            _mockGateway.Setup(x => x.GetCallbacks(reqParams)).Returns(stubbedRequests.ToList());
            var response = _classUnderTest.Execute(reqParams);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(stubbedRequests.ToResponse());
        }

        [Test]
        public void ReturnsAllCallbacksIfNoParamsProvided()
        {
            var reqParams = new CallbackRequestParams();
            var stubbedRequests = _fixture.CreateMany<HelpRequestEntity>();
            var expectedResponse = stubbedRequests.ToResponse();
            _mockGateway.Setup(x => x.GetCallbacks(reqParams)).Returns(stubbedRequests.ToList());
            var response = _classUnderTest.Execute(reqParams);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
