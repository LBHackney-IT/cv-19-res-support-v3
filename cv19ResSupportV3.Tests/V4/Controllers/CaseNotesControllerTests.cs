using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using cv19ResSupportV3.V4.Boundary.Requests;
using cv19ResSupportV3.V4.Controllers;
using cv19ResSupportV3.V4.UseCase.Enumeration;
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
        private Mock<IGetCaseNotesByResidentIdUseCase> _getCaseNotesByResidentIdUseCase;
        private Mock<IGetCaseNotesByHelpRequestIdUseCase> _getCaseNotesByHelpRequestIdUseCase;

        [SetUp]
        public void SetUp()
        {
            _createCaseNoteUseCase = new Mock<ICreateCaseNoteUseCase>();
            _getCaseNotesByResidentIdUseCase = new Mock<IGetCaseNotesByResidentIdUseCase>();
            _getCaseNotesByHelpRequestIdUseCase = new Mock<IGetCaseNotesByHelpRequestIdUseCase>();
            _classUnderTest = new CaseNotesController(_createCaseNoteUseCase.Object, _getCaseNotesByResidentIdUseCase.Object, _getCaseNotesByHelpRequestIdUseCase.Object);
        }

        [Test]
        public void CreateReturnsResponseWithStatus()
        {
            var residentId = 100;
            var helpRequestId = 2;
            var request = new CreateCaseNoteRequest() { CaseNote = "{\"author\": \"Name\", caseNote: \"note\" }" };
            _createCaseNoteUseCase.Setup(uc => uc.Execute(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns(new ResidentCaseNote() { Id = 1 });
            var response = _classUnderTest.CreateCaseNote(residentId, helpRequestId, request) as CreatedResult;
            _createCaseNoteUseCase.Verify(m => m.Execute(residentId, helpRequestId, It.IsAny<string>()), Times.Once);
            response.StatusCode.Should().Be(201);
        }

        [Test]
        public void GetByResidentIdReturnsResponseWithStatus()
        {
            _getCaseNotesByResidentIdUseCase.Setup(uc => uc.Execute(It.IsAny<int>(), HelpTypes.Excluded))
                .Returns(new List<ResidentCaseNote>() { new ResidentCaseNote() { Id = 1 } });
            var response = _classUnderTest.GetCaseNotesByResidentId(1, null) as OkObjectResult;
            response.StatusCode.Should().Be(200);
        }

        [Test]
        public void GetByHelpRequestIdReturnsResponseWithStatus()
        {
            _getCaseNotesByHelpRequestIdUseCase.Setup(uc => uc.Execute(It.IsAny<int>(), HelpTypes.Excluded))
                .Returns(new List<ResidentCaseNote>() { new ResidentCaseNote() { Id = 1 } });
            var response = _classUnderTest.GetCaseNotesByHelpRequestId(1, 1, null) as OkObjectResult;
            response.StatusCode.Should().Be(200);
        }
    }
}

