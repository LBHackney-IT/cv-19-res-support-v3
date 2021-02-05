using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using cv19ResSupportV3.V4.Boundary.Requests;
using cv19ResSupportV3.V4.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.Controllers
{
    [TestFixture]
    public class CaseNotesControllerTests
    {
        private CaseNotesController _classUnderTest;
        private Mock<ICreateCaseNoteUseCase> _createCaseNoteUseCase;

        [SetUp]
        public void SetUp()
        {
            _createCaseNoteUseCase = new Mock<ICreateCaseNoteUseCase>();
            _classUnderTest = new CaseNotesController(_createCaseNoteUseCase.Object);
        }

        [Test]
        public void CreateReturnsResponseWithStatus()
        {
            var request = new CreateCaseNoteRequest() {CaseNote = "{\"author\": \"Name\", caseNote: \"note\" }"};
            _createCaseNoteUseCase.Setup(uc => uc.Execute(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new ResidentCaseNote(){Id = 1});
            var response = _classUnderTest.CreateCaseNote(1, 1, request) as CreatedResult;
            response.StatusCode.Should().Be(201);
        }
    }
}

