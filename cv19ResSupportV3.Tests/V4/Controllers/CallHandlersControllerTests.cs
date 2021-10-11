using AutoFixture;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V4.UseCase.Interfaces;
using cv19ResSupportV3.V4;
using cv19ResSupportV3.V4.Controllers;
using cv19ResSupportV3.V4.UseCase.Interface;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace cv19ResSupportV3.Tests.V4.Controllers
{
    [TestFixture]
    public class CallHandlersControllerTests
    {
        private CallHandlersController _classUnderTest;
        private Mock<IGetCallHandlersUseCase> _getCallHandlersUseCase;

        [SetUp]
        public void SetUp()
        {
            _getCallHandlersUseCase = new Mock<IGetCallHandlersUseCase>();
            _classUnderTest = new CallHandlersController(_getCallHandlersUseCase.Object);
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
    }
}
