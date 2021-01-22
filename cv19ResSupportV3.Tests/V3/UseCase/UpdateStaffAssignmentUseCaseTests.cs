using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase;
using FluentAssertions;
using LBHFSSPublicAPI.Tests.TestHelpers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace cv19ResSupportV3.Tests.V3.UseCase
{
    public class UpdateStaffAssignmentsUseCaseTests
    {
        private Mock<IHelpRequestGateway> _mockGateway;
        private UpdateStaffAssignmentsUseCase _classUnderTest;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IHelpRequestGateway>();
            _classUnderTest = new UpdateStaffAssignmentsUseCase(_mockGateway.Object);

        }

        [Test]
        public void CallsTheGatewayGetCallbacksMethod()
        {
            var request = Randomm.Create<UpdateStaffAssignmentsRequestBoundary>();
            _mockGateway.Setup(x => x.GetCallbacks(It.IsAny<CallbackQuery>())).Returns(Randomm.CreateMany<HelpRequestWithResident>().ToList());
            _classUnderTest.Execute(request);
            _mockGateway.Verify(g => g.GetCallbacks(It.IsAny<CallbackQuery>()), Times.Once());
        }

        [Test]
        public void CallsTheGatewayPatchHelpRequestMethodForOnlyUnassignedCallbacks()
        {
            var request = Randomm.Create<UpdateStaffAssignmentsRequestBoundary>();
            var helpRequests = Randomm.CreateMany<HelpRequestWithResident>().ToList();
            helpRequests[0].AssignedTo = null;
            _mockGateway.Setup(x => x.GetCallbacks(It.IsAny<CallbackQuery>())).Returns(helpRequests);
            _classUnderTest.Execute(request);
            _mockGateway.Verify(g => g.PatchHelpRequest(It.IsAny<int>(), It.IsAny<PatchHelpRequest>()), Times.Once());
        }

        [Test]
        public void DoesNotCallTheGatewayPatchHelpRequestMethodIfNoCallbacks()
        {
            var request = Randomm.Create<UpdateStaffAssignmentsRequestBoundary>();
            _mockGateway.Setup(x => x.GetCallbacks(It.IsAny<CallbackQuery>())).Returns(new List<HelpRequestWithResident>());
            _classUnderTest.Execute(request);
            _mockGateway.Verify(g => g.PatchHelpRequest(It.IsAny<int>(), It.IsAny<PatchHelpRequest>()), Times.Never());
        }

    }
}
