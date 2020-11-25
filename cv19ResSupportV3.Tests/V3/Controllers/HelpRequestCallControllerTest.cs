using System;
using System.Collections.Generic;
using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Controllers;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.UseCase;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace cv19ResRupportV3.Tests.V3.Controllers
{

    [TestFixture]
    public class HelpRequestCallControllerTests
    {
        private HelpRequestCallController _classUnderTest;
        private Mock<ICreateHelpRequestCallUseCase> _fakeCreateHelpRequestCallUseCase;
        private Mock<IGetHelpRequestCallsUseCase> _fakeGetHelpRequestCallsUseCase;

        [SetUp]
        public void SetUp()
        {
            _fakeCreateHelpRequestCallUseCase = new Mock<ICreateHelpRequestCallUseCase>();
            _fakeGetHelpRequestCallsUseCase = new Mock<IGetHelpRequestCallsUseCase>();
            _classUnderTest = new HelpRequestCallController(_fakeCreateHelpRequestCallUseCase.Object, _fakeGetHelpRequestCallsUseCase.Object);
        }

        [Test]
        public void ReturnsResponseWithStatus()
        {
            var helpRequest = new Fixture().Build<HelpRequest>()
                .Create();
            var request = new Fixture().Build<HelpRequestCall>().Create();
            _fakeCreateHelpRequestCallUseCase.Setup(x => x.Execute(helpRequest.Id, request))
                .Returns(new HelpRequestCallCreateResponse() { Id = request.Id });
            var response = _classUnderTest.CreateHelpRequestCall(helpRequest.Id, request) as CreatedResult;
            response.StatusCode.Should().Be(201);
        }


        [Test]
        public void GetCallsEndpointReturnsResponseWithStatus()
        {
            var id = 1;
            _fakeGetHelpRequestCallsUseCase.Setup(x => x.Execute(id))
                .Returns(new List<CallGetResponse>() { });
            var response = _classUnderTest.GetCalls(id) as OkObjectResult; ;
            _fakeGetHelpRequestCallsUseCase.Verify(mock => mock.Execute(id), Times.Once());
            response.StatusCode.Should().Be(200);
        }

        [Test]
        public void GetCallsReturns404InCaseOfInvalidOperationException()
        {
            var id = 112;
            _fakeGetHelpRequestCallsUseCase.Setup(x => x.Execute(id))
                .Throws(new InvalidOperationException());
            var response = _classUnderTest.GetCalls(id) as NotFoundObjectResult;
            response.StatusCode.Should().Be(404);
        }

        [Test]
        public void GetCallsReturns400InCaseOfException()
        {
            var id = 112;
            _fakeGetHelpRequestCallsUseCase.Setup(x => x.Execute(id))
                .Throws(new Exception());
            var response = _classUnderTest.GetCalls(id) as BadRequestObjectResult;
            response.StatusCode.Should().Be(400);
        }
    }
}
