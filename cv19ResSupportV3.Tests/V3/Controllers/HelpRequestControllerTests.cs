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
    public class HelpRequestsControllerTests
    {
        private HelpRequestsController _classUnderTest;
        private Mock<ICreateHelpRequestUseCase> _fakeCreateHelpRequestUseCase;
        private Mock<IUpdateHelpRequestUseCase> _fakeUpdateHelpRequestUseCase;
        private Mock<IPatchHelpRequestUseCase> _fakePatchHelpRequestUseCase;
        private Mock<IGetHelpRequestsUseCase> _fakeGetHelpRequestsUseCase;
        private Mock<IGetHelpRequestUseCase> _fakeGetHelpRequestUseCase;

        [SetUp]
        public void SetUp()
        {
            _fakeCreateHelpRequestUseCase = new Mock<ICreateHelpRequestUseCase>();
            _fakeUpdateHelpRequestUseCase = new Mock<IUpdateHelpRequestUseCase>();
            _fakePatchHelpRequestUseCase = new Mock<IPatchHelpRequestUseCase>();
            _fakeGetHelpRequestsUseCase = new Mock<IGetHelpRequestsUseCase>();
            _fakeGetHelpRequestUseCase = new Mock<IGetHelpRequestUseCase>();
            _classUnderTest = new HelpRequestsController(_fakeCreateHelpRequestUseCase.Object,
                _fakeGetHelpRequestsUseCase.Object, _fakeUpdateHelpRequestUseCase.Object,
                _fakeGetHelpRequestUseCase.Object, _fakePatchHelpRequestUseCase.Object);
        }

        [Test]
        public void ReturnsResponseWithStatus()
        {
            var request = new Fixture().Build<HelpRequest>().Create();
            _fakeCreateHelpRequestUseCase.Setup(x => x.Execute(request))
                .Returns(new HelpRequestCreateResponse(){Id = request.Id.Value});
            var response = _classUnderTest.CreateHelpRequest(request) as CreatedResult;
            response.StatusCode.Should().Be(201);
        }
    }
}
