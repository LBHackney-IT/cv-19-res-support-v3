using System.Linq;
using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.UseCases
{
    [TestFixture]
    public class GetCaseNotesByResidentIdUseCaseTests
    {
        private Mock<ICaseNotesGateway> _mockGateway;
        private GetCaseNotesByResidentIdUseCase _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _mockGateway = new Mock<ICaseNotesGateway>();
            _classUnderTest = new GetCaseNotesByResidentIdUseCase(_mockGateway.Object);
        }

        [Test]
        public void ExecuteMethodCallsResidentGateway()
        {
            var gatewayResponse = new Fixture().Build<ResidentCaseNote>().CreateMany().ToList();
            _mockGateway.Setup(gw => gw.GetByResidentId(It.IsAny<int>())).Returns(gatewayResponse);
            var response = _classUnderTest.Execute(1);
            _mockGateway.Verify(uc => uc.GetByResidentId(1), Times.Once);
            response.Should().BeEquivalentTo(gatewayResponse.ToResponse());
        }

    }
}
