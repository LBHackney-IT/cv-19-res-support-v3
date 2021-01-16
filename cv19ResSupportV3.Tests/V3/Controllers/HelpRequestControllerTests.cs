using System;
using System.Collections.Generic;
using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Controllers;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories;
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
        private Mock<IGetResidentsAndHelpRequestsUseCase> _fakeGetResidentsAndHelpRequestsUseCase;
        private Mock<IGetResidentAndHelpRequestUseCase> _fakeGetResidentAndHelpRequestUseCase;
        private Mock<ICreateResidentAndHelpRequestUseCase> _fakeCreateResidentAndHelpRequestUseCase;

        [SetUp]
        public void SetUp()
        {
            _fakeCreateHelpRequestUseCase = new Mock<ICreateHelpRequestUseCase>();
            _fakeUpdateHelpRequestUseCase = new Mock<IUpdateHelpRequestUseCase>();
            _fakePatchHelpRequestUseCase = new Mock<IPatchHelpRequestUseCase>();
            _fakeGetResidentsAndHelpRequestsUseCase = new Mock<IGetResidentsAndHelpRequestsUseCase>();
            _fakeGetResidentAndHelpRequestUseCase = new Mock<IGetResidentAndHelpRequestUseCase>();
            _fakeCreateResidentAndHelpRequestUseCase = new Mock<ICreateResidentAndHelpRequestUseCase>();
            _classUnderTest = new HelpRequestsController(_fakeCreateHelpRequestUseCase.Object, _fakeGetResidentsAndHelpRequestsUseCase.Object, _fakeUpdateHelpRequestUseCase.Object,
            _fakeGetResidentAndHelpRequestUseCase.Object, _fakePatchHelpRequestUseCase.Object, _fakeCreateResidentAndHelpRequestUseCase.Object);
        }

        [Test]
        public void CreateReturnsResponseWithStatus()
        {
            var request = new Fixture().Build<HelpRequestCreateRequestBoundary>().Create();
            _fakeCreateResidentAndHelpRequestUseCase.Setup(x => x.Execute(It.IsAny<CreateResidentAndHelpRequest>()))
                .Returns(3);
            var response = _classUnderTest.CreateResidentAndHelpRequest(request) as CreatedResult;
            _fakeCreateResidentAndHelpRequestUseCase.Verify(m => m.Execute(It.IsAny<CreateResidentAndHelpRequest>()), Times.Once());
            response.StatusCode.Should().Be(201);
        }

        [Test]
        public void GetResidentAndHelpRequestReturnsResponseWithStatus()
        {
            var id = 1;
            _fakeGetResidentAndHelpRequestUseCase.Setup(x => x.Execute(It.IsAny<int>()))
                .Returns(new HelpRequestWithResident() { Id = 1 });
            var response = _classUnderTest.GetResidentAndHelpRequest(id) as OkObjectResult;
            _fakeGetResidentAndHelpRequestUseCase.Verify(m => m.Execute(It.Is<int>(x => x == id)), Times.Once());
            response.StatusCode.Should().Be(200);
        }

        [Test]
        public void GetResidentsAndHelpRequestsReturnsResponseWithStatus()
        {
            var searchParams = new RequestQueryParams() { Postcode = "ABC" };
            _fakeGetResidentsAndHelpRequestsUseCase.Setup(x => x.Execute(It.IsAny<SearchRequest>()))
                .Returns(new List<HelpRequestWithResident>());
            var response = _classUnderTest.GetResidentsAndHelpRequests(searchParams) as OkObjectResult;
            _fakeGetResidentsAndHelpRequestsUseCase.Verify(m => m.Execute(It.IsAny<SearchRequest>()), Times.Once());
            response.StatusCode.Should().Be(200);
        }
    }
}
