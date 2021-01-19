using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
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
    public class CreateCaseNoteUseCaseTests
    {
        private Mock<ICaseNotesGateway> _mockGateway;
        private CreateCaseNoteUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<ICaseNotesGateway>();
            _classUnderTest = new CreateCaseNoteUseCase(_mockGateway.Object);
        }

        [Test]
        public void ExecuteSavesRequestToDatabase()
        {
            var expectedResponse = new ResidentCaseNote() { Id = 1, ResidentId = 1, HelpRequestId = 2, CaseNote = "data-to-save" };
            _mockGateway.Setup(s => s.CreateCaseNote(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(expectedResponse);
            var response = _classUnderTest.Execute(expectedResponse.HelpRequestId, expectedResponse.ResidentId, expectedResponse.CaseNote);
            _mockGateway.Verify(m => m.CreateCaseNote(expectedResponse.HelpRequestId, expectedResponse.ResidentId, expectedResponse.CaseNote), Times.Once());
            response.Should().BeEquivalentTo(expectedResponse);
        }
    }
}
