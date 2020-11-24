using System.Collections.Generic;
using cv19ResSupportV3.Tests.V3.Helpers;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V3.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.UseCase
{
    public class GetHelpRequestCallsUseCaseTests
    {
        private Mock<IHelpRequestCallGateway> _mockGateway;
        private GetHelpRequestCallsUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestCallGateway>();
            _classUnderTest = new GetHelpRequestCallsUseCase(_mockGateway.Object);
        }

        [Test]
        public void ReturnsPopulatedHelpRequestListIfParamsProvided()
        {
            var id = 1;
            var stubbedResponse = new List<HelpRequestCallEntity>() { EntityHelpers.createHelpRequestCallEntity() };
            _mockGateway.Setup(x => x.GetHelpRequestCalls(id)).Returns(stubbedResponse);
            var response = _classUnderTest.Execute(id);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(stubbedResponse.ToResponse());
        }
    }
}
