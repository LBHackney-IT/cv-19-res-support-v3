using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V4;
using cv19ResSupportV3.V4.Controllers;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.UseCase.Interface;
using cv19ResSupportV3.V4.UseCase.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V4.Controllers
{
    [TestFixture]
    public class ResidentsControllerTests
    {
        private ResidentsController _classUnderTest;
        private Mock<IGetResidentsUseCase> _getResidentsUseCase;
        private Mock<ICreateResidentsUseCase> _createResidentsUseCase;
        private Mock<IPatchResidentUseCase> _patchResidentUseCase;
        private Mock<ISearchResidentsUseCase> _searchResidentUseCase;

        [SetUp]
        public void SetUp()
        {
            _getResidentsUseCase = new Mock<IGetResidentsUseCase>();
            _createResidentsUseCase = new Mock<ICreateResidentsUseCase>();
            _patchResidentUseCase = new Mock<IPatchResidentUseCase>();
            _searchResidentUseCase = new Mock<ISearchResidentsUseCase>();
            _classUnderTest = new ResidentsController(
                _createResidentsUseCase.Object,
                _getResidentsUseCase.Object,
                _patchResidentUseCase.Object,
                _searchResidentUseCase.Object);
        }

        [Test]
        public void CreateReturnsResponseWithStatus()
        {
            var request = new Fixture().Build<ResidentRequestBoundary>().Create();
            _createResidentsUseCase.Setup(uc => uc.Execute(It.IsAny<ResidentRequestBoundary>()))
                .Returns(request.ToResident().ToResponse);
            var response = _classUnderTest.CreateResident(request) as CreatedResult;
            response.StatusCode.Should().Be(201);
        }

        [Test]
        public void CreateResidentCallsCreateResidentUseCaseExecuteMethod()
        {
            var request = new Fixture().Build<ResidentRequestBoundary>().Create();
            _createResidentsUseCase.Setup(uc => uc.Execute(It.IsAny<ResidentRequestBoundary>()))
                .Returns(request.ToResident().ToResponse);
            _classUnderTest.CreateResident(request);
            _createResidentsUseCase.Verify(uc => uc.Execute(It.IsAny<ResidentRequestBoundary>()), Times.Once);
        }

        [Test]
        public void GetReturnsResponseWithStatus()
        {
            var response = new Fixture().Build<ResidentResponseBoundary>().Create();
            _getResidentsUseCase.Setup(uc => uc.Execute(It.IsAny<int>()))
                .Returns(response);
            var result = _classUnderTest.GetResident(response.Id) as OkObjectResult;
            result.StatusCode.Should().Be(200);
        }


    }
}
