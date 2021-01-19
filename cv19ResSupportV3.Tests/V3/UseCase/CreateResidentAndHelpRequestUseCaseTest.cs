using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.UseCase;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.UseCase
{
    [TestFixture]
    public class CreateResidentAndHelpRequestUseCaseTest
    {
        private Mock<ICreateResidentUseCase> _fakeCreateResidentUseCase;
        private Mock<ICreateHelpRequestUseCase> _fakeCreateHelpRequestUseCase;
        private Mock<ICreateCaseNoteUseCase> _fakeCreateCaseNoteUseCase;
        private CreateResidentAndHelpRequestUseCase _classUnderTest;


        [SetUp]
        public void SetUp()
        {
            _fakeCreateResidentUseCase = new Mock<ICreateResidentUseCase>();
            _fakeCreateHelpRequestUseCase = new Mock<ICreateHelpRequestUseCase>();
            _fakeCreateCaseNoteUseCase = new Mock<ICreateCaseNoteUseCase>();
            _classUnderTest = new CreateResidentAndHelpRequestUseCase(_fakeCreateResidentUseCase.Object, _fakeCreateHelpRequestUseCase.Object, _fakeCreateCaseNoteUseCase.Object);
        }

        [Test]
        public void CreatesANewResidentIfItDoesntExistAndSavesANewHelpRequest()
        {
            _fakeCreateResidentUseCase.Setup(s => s.Execute(It.IsAny<CreateResident>())).Returns(new Resident() { Id = 1 });
            _fakeCreateHelpRequestUseCase.Setup(s => s.Execute(It.IsAny<int>(), It.IsAny<CreateHelpRequest>())).Returns(2);
            _fakeCreateCaseNoteUseCase.Setup(s => s.Execute(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Verifiable();

            var dataToSave = new Fixture().Build<CreateResidentAndHelpRequest>().Create();
            dataToSave.CaseNotes = "saved";
            var response = _classUnderTest.Execute(dataToSave);
            _fakeCreateResidentUseCase.Verify(m => m.Execute(It.IsAny<CreateResident>()), Times.Once());
            _fakeCreateHelpRequestUseCase.Verify(m => m.Execute(It.Is<int>(x => x == 1), It.IsAny<CreateHelpRequest>()), Times.Once());

            _fakeCreateCaseNoteUseCase.Verify(m => m.Execute(2, 1, "saved"), Times.Once());
            response.Should().Be(2);
        }
    }
}

