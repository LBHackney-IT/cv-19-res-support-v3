using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.UseCase;
using cv19ResSupportV3.V4.UseCase.Enumeration;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.UseCases
{
    [TestFixture]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Justifiable for unit tests to separate between Method, Condition, Result.")]
    public class GetCaseNotesByHelpRequestIdUseCaseTests
    {
        private Mock<ICaseNotesGateway> _mockGateway;
        private GetCaseNotesByHelpRequestIdUseCase _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _mockGateway = new Mock<ICaseNotesGateway>();
            _classUnderTest = new GetCaseNotesByHelpRequestIdUseCase(_mockGateway.Object);
        }

        [Test]
        public void ExecuteMethodCallsResidentGateway()
        {
            var gatewayResponse = new Fixture().Build<ResidentCaseNote>().CreateMany().ToList();
            _mockGateway.Setup(gw => gw.GetByHelpRequestId(It.IsAny<int>())).Returns(gatewayResponse);
            var response = _classUnderTest.Execute(1);
            _mockGateway.Verify(uc => uc.GetByHelpRequestId(1), Times.Once);
            response.Should().BeEquivalentTo(gatewayResponse.ToResponse());
        }

        [Test]
        public void Execute_ExcludedHelpTypes_ReturnsFilteredList()
        {
            var gatewayResponse = new List<ResidentCaseNote>()
            {
                new ResidentCaseNote() {
                    HelpNeeded = HelpTypes.Excluded.First()
                },
                new ResidentCaseNote() {
                    HelpNeeded = "Contact Tracing"
                },
            };

            _mockGateway.Setup(gw => gw.GetByHelpRequestId(It.IsAny<int>())).Returns(gatewayResponse);

            var response = _classUnderTest.Execute(3);

            response.Should().BeEquivalentTo(gatewayResponse.Where(x => !HelpTypes.Excluded.Contains(x.HelpNeeded)));
        }

        [Test]
        public void Execute_NoExcludedHelpTypes_ReturnsList()
        {
            var gatewayResponse = new List<ResidentCaseNote>()
            {
                new ResidentCaseNote() {
                    HelpNeeded = "Contact Tracing"
                },
                new ResidentCaseNote() {
                    HelpNeeded = "Contact Tracing"
                },
            };

            _mockGateway.Setup(gw => gw.GetByHelpRequestId(It.IsAny<int>())).Returns(gatewayResponse);

            var response = _classUnderTest.Execute(3);

            response.Should().BeEquivalentTo(gatewayResponse);
        }
    }
}
