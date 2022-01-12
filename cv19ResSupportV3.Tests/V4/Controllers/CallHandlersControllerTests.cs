using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V4;
using cv19ResSupportV3.V4.Controllers;
using cv19ResSupportV3.V4.UseCase.Interface;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.Tests.V4.Controllers
{
    [TestFixture]
    public class CallHandlersControllerTests
    {
        private CallHandlersController _classUnderTest;
        private Mock<IGetCallHandlersUseCase> _getCallHandlersUseCase;
        private Mock<IUpsertCallHandlerUseCase> _upsertCallHandlersUseCase;
        private Mock<IDeleteCallHandlerUseCase> _deleteCallHandlerUseCase;

        [SetUp]
        public void SetUp()
        {
            _getCallHandlersUseCase = new Mock<IGetCallHandlersUseCase>();
            _upsertCallHandlersUseCase = new Mock<IUpsertCallHandlerUseCase>();
            _deleteCallHandlerUseCase = new Mock<IDeleteCallHandlerUseCase>();
            _classUnderTest = new CallHandlersController(
                _getCallHandlersUseCase.Object,
                _upsertCallHandlersUseCase.Object,
                _deleteCallHandlerUseCase.Object);
        }

        [Test]
        public void GetReturnsResponseWithStatus()
        {
            var response = new Fixture().Build<List<CallHandlerResponseBoundary>>().Create();
            _getCallHandlersUseCase.Setup(uc => uc.Execute())
                .Returns(response);
            var result = _classUnderTest.GetCallHandlers() as OkObjectResult;
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public void GetEmptyListResponseWithStatus()
        {
            var response = new List<CallHandlerResponseBoundary>();
            _getCallHandlersUseCase.Setup(uc => uc.Execute())
                .Returns(response);
            var result = _classUnderTest.GetCallHandlers() as OkObjectResult;
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public void CreateReturnsResponseWithStatus()
        {
            var request = new Fixture().Build<CreateCallHandlerRequestBoundary>().Create();
            _upsertCallHandlersUseCase.Setup(uc => uc.Execute(It.IsAny<CallHandlerCommand>()))
                .Returns(new CallHandlerResponse());
            var response = _classUnderTest.CreateCallHandler(request) as CreatedResult;
            response.StatusCode.Should().Be(201);
        }

        [Test]
        public void CreateCallHandlerCallsUseCaseExecuteMethod()
        {
            var request = new Fixture().Build<CreateCallHandlerRequestBoundary>().Create();
            _upsertCallHandlersUseCase.Setup(uc => uc.Execute(It.IsAny<CallHandlerCommand>()))
                .Returns(new CallHandlerResponse());
            _classUnderTest.CreateCallHandler(request);
            _upsertCallHandlersUseCase.Verify(uc => uc.Execute(It.IsAny<CallHandlerCommand>()), Times.Once);
        }

        [Test]
        public void PutReturnsResponseWithStatus()
        {
            var request = new Fixture().Build<PutCallHandlerRequestBoundary>().Create();
            _upsertCallHandlersUseCase.Setup(uc => uc.Execute(It.IsAny<CallHandlerCommand>()))
                .Returns(new CallHandlerResponse());
            var response = _classUnderTest.PutCallHandler(request) as OkObjectResult;
            response.StatusCode.Should().Be(200);
        }

        [Test]
        public void PutCallHandlerCallsUseCaseExecuteMethod()
        {
            var request = new Fixture().Build<PutCallHandlerRequestBoundary>().Create();
            _upsertCallHandlersUseCase.Setup(uc => uc.Execute(It.IsAny<CallHandlerCommand>()))
                .Returns(new CallHandlerResponse());
            _classUnderTest.PutCallHandler(request);
            _upsertCallHandlersUseCase.Verify(uc => uc.Execute(It.IsAny<CallHandlerCommand>()), Times.Once);
        }

        [Test]
        public void DeleteReturnsResponseWithStatus()
        {
            _deleteCallHandlerUseCase.Setup(uc => uc.Execute(It.IsAny<int>()))
                .Returns(true);
            var response = _classUnderTest.DeleteCallHandler(1) as OkObjectResult;
            response.StatusCode.Should().Be(200);
        }

        [Test]
        public void DeleteCallHandlerCallsUseCaseExecuteMethod()
        {
            _deleteCallHandlerUseCase.Setup(uc => uc.Execute(It.IsAny<int>()))
                .Returns(true);
            _classUnderTest.DeleteCallHandler(1);
            _deleteCallHandlerUseCase.Verify(uc => uc.Execute(It.IsAny<int>()), Times.Once);
        }

        [Test]
        public void DeleteIdDoesNotExistReturnsBadRequest()
        {
            _deleteCallHandlerUseCase.Setup(uc => uc.Execute(It.IsAny<int>()))
                .Returns(false);
            var response = _classUnderTest.DeleteCallHandler(1) as BadRequestObjectResult;
            response.StatusCode.Should().Be(400);
        }
    }
}
