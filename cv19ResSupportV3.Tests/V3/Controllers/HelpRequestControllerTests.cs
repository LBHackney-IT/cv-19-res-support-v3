using System.Collections.Generic;
using AutoFixture;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Controllers;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.UseCase;
using cv19ResSupportV3.V3.UseCase.Interfaces;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Moq;

namespace cv19ResRupportV3.Tests.V3.Controllers
{

    [TestFixture]
    public class HelpRequestsControllerTests
    {
        private HelpRequestsController _classUnderTest;
        private Mock<IUpdateResidentAndHelpRequestUseCase> _fakeUpdateResidentAndHelpRequestUseCase;
        private Mock<IPatchResidentAndHelpRequestUseCase> _fakePatchResidentAndHelpRequestUseCase;
        private Mock<IGetResidentsAndHelpRequestsUseCase> _fakeGetResidentsAndHelpRequestsUseCase;
        private Mock<IGetResidentAndHelpRequestUseCase> _fakeGetResidentAndHelpRequestUseCase;
        private Mock<ICreateResidentAndHelpRequestUseCase> _fakeCreateResidentAndHelpRequestUseCase;
        private Mock<IUpdateStaffAssignmentsUseCase> _fakeUpdateStaffAssignmentUseCase;

        [SetUp]
        public void SetUp()
        {
            _fakeUpdateResidentAndHelpRequestUseCase = new Mock<IUpdateResidentAndHelpRequestUseCase>();
            _fakePatchResidentAndHelpRequestUseCase = new Mock<IPatchResidentAndHelpRequestUseCase>();
            _fakeGetResidentsAndHelpRequestsUseCase = new Mock<IGetResidentsAndHelpRequestsUseCase>();
            _fakeGetResidentAndHelpRequestUseCase = new Mock<IGetResidentAndHelpRequestUseCase>();
            _fakeCreateResidentAndHelpRequestUseCase = new Mock<ICreateResidentAndHelpRequestUseCase>();
            _fakeUpdateStaffAssignmentUseCase = new Mock<IUpdateStaffAssignmentsUseCase>();
            _classUnderTest = new HelpRequestsController(_fakeGetResidentsAndHelpRequestsUseCase.Object, _fakeUpdateResidentAndHelpRequestUseCase.Object,
            _fakeGetResidentAndHelpRequestUseCase.Object, _fakePatchResidentAndHelpRequestUseCase.Object, _fakeCreateResidentAndHelpRequestUseCase.Object,
            _fakeUpdateStaffAssignmentUseCase.Object);
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

        [Test]
        public void PatchResidentsAndHelpRequestsReturnsResponseWithStatus()
        {
            var searchParams = new HelpRequestPatchRequest() { PostCode = "B1" };
            _fakePatchResidentAndHelpRequestUseCase.Setup(x => x.Execute(It.IsAny<int>(), It.IsAny<PatchResidentAndHelpRequest>())).Verifiable();
            var response = _classUnderTest.PatchResidentAndHelpRequest(1, searchParams) as OkObjectResult;
            _fakePatchResidentAndHelpRequestUseCase.Verify(m => m.Execute(It.Is<int>(x => x == 1), It.IsAny<PatchResidentAndHelpRequest>()), Times.Once());
            response.StatusCode.Should().Be(200);
        }

        [Test]
        public void AssignStaffToHelpRequestWithValidParametersReturnsResponseWithOkStatus()
        {
            var request = Randomm.Create<UpdateStaffAssignmentsRequestBoundary>();
            var response = _classUnderTest.UpdateStaffAssignments(request) as OkResult;
            _fakeUpdateStaffAssignmentUseCase.Verify(m => m.Execute(It.Is<UpdateStaffAssignmentsRequestBoundary>(x => x.HelpNeeded == request.HelpNeeded)), Times.Once());
            response.StatusCode.Should().Be(200);
        }

        [Test]
        public void AssignStaffToHelpRequestWithoutHelpNeededParametersReturnsResponseWithBadRequestStatus()
        {
            var request = Randomm.Create<UpdateStaffAssignmentsRequestBoundary>();
            request.HelpNeeded = "";
            var response = _classUnderTest.UpdateStaffAssignments(request) as BadRequestObjectResult;
            _fakeUpdateStaffAssignmentUseCase.Verify(m => m.Execute(It.IsAny<UpdateStaffAssignmentsRequestBoundary>()), Times.Never);
            response.StatusCode.Should().Be(400);
        }
    }
}
