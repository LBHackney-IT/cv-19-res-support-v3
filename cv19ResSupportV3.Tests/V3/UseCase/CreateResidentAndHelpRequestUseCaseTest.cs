using AutoFixture;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
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
        private Mock<IFindResidentUseCase> _fakeFindResidentUseCase;
        private Mock<ICreateResidentUseCase> _fakeCreateResidentUseCase;
        private Mock<ICreateHelpRequestUseCase> _fakeCreateHelpRequestUseCase;
        private CreateResidentAndHelpRequestUseCase _classUnderTest;


        [SetUp]
        public void SetUp()
        {
            _fakeFindResidentUseCase = new Mock<IFindResidentUseCase>();
            _fakeCreateResidentUseCase = new Mock<ICreateResidentUseCase>();
            _fakeCreateHelpRequestUseCase = new Mock<ICreateHelpRequestUseCase>();
            _classUnderTest = new CreateResidentAndHelpRequestUseCase(_fakeFindResidentUseCase.Object, _fakeCreateResidentUseCase.Object, _fakeCreateHelpRequestUseCase.Object);
        }

        [Test]
        public void SavesANewResidentIfItDoesntExistAndSavesANewHelpRequest()
        {
            _fakeFindResidentUseCase.Setup(s => s.Execute(It.IsAny<FindResident>())).Returns(null);
            _fakeCreateResidentUseCase.Setup(s => s.Execute(It.IsAny<CreateResident>())).Returns(1);
            _fakeCreateHelpRequestUseCase.Setup(s => s.Execute(It.IsAny<CreateHelpRequest>())).Returns(2);
            var dataToSave = new Fixture().Build<CreateResidentAndHelpRequest>().Create();
            var response = _classUnderTest.Execute(dataToSave);
            _fakeFindResidentUseCase.Verify(m => m.Execute(It.IsAny<FindResident>()), Times.Once());
            _fakeCreateResidentUseCase.Verify(m => m.Execute(It.IsAny<CreateResident>()), Times.Once());
            _fakeCreateHelpRequestUseCase.Verify(m => m.Execute(It.IsAny<CreateHelpRequest>()), Times.Once());

            response.Should().Be(2);
        }
    }
}

