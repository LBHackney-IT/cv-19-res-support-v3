using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
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
        private Mock<ICreateResidentUseCase> _fakeCreateResidentUseCase;
        private Mock<ICreateHelpRequestUseCase> _fakeCreateHelpRequestUseCase;
        private CreateResidentAndHelpRequestUseCase _classUnderTest;


        [SetUp]
        public void SetUp()
        {
            _fakeCreateResidentUseCase = new Mock<ICreateResidentUseCase>();
            _fakeCreateHelpRequestUseCase = new Mock<ICreateHelpRequestUseCase>();

            _classUnderTest = new CreateResidentAndHelpRequestUseCase(_fakeCreateResidentUseCase.Object, _fakeCreateHelpRequestUseCase.Object);
        }

        [Test]
        public void CreatesANewResidentIfItDoesntExistAndSavesANewHelpRequest()
        {
            _fakeCreateResidentUseCase.Setup(s => s.Execute(It.IsAny<CreateResident>())).Returns(new Resident(){Id = 1});
            _fakeCreateHelpRequestUseCase.Setup(s => s.Execute(It.IsAny<int>(), It.IsAny<CreateHelpRequest>())).Returns(2);

            var dataToSave = new Fixture().Build<CreateResidentAndHelpRequest>().Create();
            var response = _classUnderTest.Execute(dataToSave);
            _fakeCreateResidentUseCase.Verify(m => m.Execute(It.IsAny<CreateResident>()), Times.Once());
            _fakeCreateHelpRequestUseCase.Verify(m => m.Execute(It.Is<int>(x => x == 1), It.IsAny<CreateHelpRequest>()), Times.Once());

            response.Should().Be(2);
        }
        //        [Test]
        //        public void MergesAResidentIfItDoesExistAndSavesANewHelpRequest()
        //        {
        //            _fakeCreateHelpRequestUseCase.Setup(s => s.Execute(It.IsAny<int>(), It.IsAny<CreateHelpRequest>())).Returns(2);
        //
        //            var dataToSave = new Fixture().Build<CreateResidentAndHelpRequest>().Create();
        //            var response = _classUnderTest.Execute(dataToSave);
        //            _fakeCreateResidentUseCase.Verify(m => m.Execute(It.IsAny<CreateResident>()), Times.Never());
        //            _fakeCreateHelpRequestUseCase.Verify(m => m.Execute(It.Is<int>(x => x==1), It.IsAny<CreateHelpRequest>()), Times.Once());
        //
        //            response.Should().Be(2);
        //        }
    }
}

