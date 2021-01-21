using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;

namespace cv19ResSupportV3.V3.UseCase
{
    public class UpdateStaffAssignmentsUseCase : IUpdateStaffAssignmentsUseCase
    {
        private readonly IHelpRequestGateway _gateway;

        public UpdateStaffAssignmentsUseCase(IHelpRequestGateway gateway)
        {
            _gateway = gateway;
        }

        public void Execute(UpdateStaffAssignmentsRequestBoundary command)
        {
            var help = command.HelpNeeded;
            var helpRequests = _gateway.GetCallbacks(new CallbackQuery{HelpNeeded = help});

            var numberOfStaff = command.StaffMembers.Count;
            var numberOfRequests = helpRequests.Count;

            var requestsPerStaff = numberOfRequests / numberOfStaff;

            for (var i=0; i < command.StaffMembers.Count; i++)
            {
                for (var ii = i * requestsPerStaff; ii < requestsPerStaff * (i + 1); ii++)
                {
                    var patchRequestObject = new PatchHelpRequest{ AssignedStaff = command.StaffMembers[i] };

                    _gateway.PatchHelpRequest(helpRequests[ii].Id, patchRequestObject);
                }
            }
        }
    }
}
