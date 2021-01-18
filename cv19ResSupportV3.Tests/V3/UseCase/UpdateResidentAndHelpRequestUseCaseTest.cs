using AutoFixture;
using cv19ResSupportV3.Tests.V3.E2ETests;
using cv19ResSupportV3.Tests.V3.Helper;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V3.UseCase;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using UpdateHelpRequest = cv19ResSupportV3.V3.Domain.Commands.UpdateHelpRequest;

namespace cv19ResSupportV3.Tests.V3.UseCase
{
    [TestFixture]
    public class UpdateResidentAndHelpRequestUseCaseTest
    {
        private Mock<IUpdateResidentUseCase> _updateResidentUseCase;
        private Mock<IUpdateHelpRequestUseCase> _updateHelpRequestUseCase;
        private UpdateResidentAndHelpRequestUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _updateResidentUseCase = new Mock<IUpdateResidentUseCase>();
            _updateHelpRequestUseCase = new Mock<IUpdateHelpRequestUseCase>();
            _classUnderTest = new UpdateResidentAndHelpRequestUseCase(_updateResidentUseCase.Object, _updateHelpRequestUseCase.Object);
        }

        [Test]
        public void ExecuteUpdatesRequestInDatabase()
        {
            var request = new UpdateResidentAndHelpRequest
            {
                Id = 7
            };
            var resident = new Resident(){FirstName = "sample"};
            var helpRequest = new HelpRequest(){ Id = request.Id, ResidentId = resident.Id};

            _updateResidentUseCase.Setup(s => s.Execute(It.IsAny<int>(), It.IsAny<UpdateResident>())).Returns(resident);
            _updateHelpRequestUseCase.Setup(s => s.Execute(It.IsAny<int>(),It.IsAny<UpdateHelpRequest>())).Returns(helpRequest);

            var response = _classUnderTest.Execute(request);

            var expectedResponse = new HelpRequestWithResident()
            {
                FirstName = resident.FirstName,
                Id = helpRequest.Id,
                ResidentId = helpRequest.ResidentId
            };
            response.Should().BeEquivalentTo(expectedResponse);


            _updateResidentUseCase.Verify(m => m.Execute(It.IsAny<int>(),It.IsAny<UpdateResident>()), Times.Once());
            _updateHelpRequestUseCase.Verify(m => m.Execute(It.IsAny<int>(),It.IsAny<UpdateHelpRequest>()), Times.Once());
        }
    }
}
