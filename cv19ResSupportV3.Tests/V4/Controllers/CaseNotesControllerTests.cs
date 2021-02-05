using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using cv19ResSupportV3.V4.Boundary.Requests;
using cv19ResSupportV3.V4.Controllers;
using cv19ResSupportV3.V4.UseCase.Interface;
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
        private Mock<IGetCaseNotesByResidentId> _getCaseNotesByResidentIdUseCase;

        [SetUp]
        public void SetUp()
        {
            _createCaseNoteUseCase = new Mock<ICreateCaseNoteUseCase>();
            _getCaseNotesByResidentIdUseCase = new Mock<IGetCaseNotesByResidentId>();
            _classUnderTest = new CaseNotesController(_createCaseNoteUseCase.Object, _getCaseNotesByResidentIdUseCase.Object);
        }

        [Test]
        public void CreateReturnsResponseWithStatus()
        {
            var request = new CreateCaseNoteRequest() { CaseNote = "{\"author\": \"Name\", caseNote: \"note\" }" };
            _createCaseNoteUseCase.Setup(uc => uc.Execute(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new ResidentCaseNote() { Id = 1 });
            var response = _classUnderTest.CreateCaseNote(1, 1, request) as CreatedResult;
            response.StatusCode.Should().Be(201);
        }

        [Test]
        public void GetByResidentIdReturnsResponseWithStatus()
        {
            _getCaseNotesByResidentIdUseCase.Setup(uc => uc.Execute(It.IsAny<int>()))
                .Returns(new List<ResidentCaseNote>() { new ResidentCaseNote() { Id = 1 } });
            var response = _classUnderTest.GetCaseNotesByResidentId(1) as OkObjectResult;
            response.StatusCode.Should().Be(200);
        }
    }
}

