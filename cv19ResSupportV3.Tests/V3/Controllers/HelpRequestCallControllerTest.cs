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

        [SetUp]
        public void SetUp()
        {
            _fakeCreateHelpRequestCallUseCase = new Mock<ICreateHelpRequestCallUseCase>();
            _classUnderTest = new HelpRequestCallController(_fakeCreateHelpRequestCallUseCase.Object);
        }

        [Test]
        public void ReturnsResponseWithStatus()
        {
            var request = new Fixture().Build<HelpRequestCall>().Create();
            _fakeCreateHelpRequestCallUseCase.Setup(x => x.Execute(request))
                .Returns(new HelpRequestCallCreateResponse(){Id = request.Id});
            var response = _classUnderTest.CreateHelpRequestCall(request) as CreatedResult;
            response.StatusCode.Should().Be(201);
        }
    }
}
