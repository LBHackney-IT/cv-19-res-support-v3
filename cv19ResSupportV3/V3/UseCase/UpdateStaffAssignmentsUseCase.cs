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
            var helpRequests = _gateway.GetCallbacks(new CallbackQuery { HelpNeeded = help });

            var numberOfStaff = command.StaffMembers.Count;
            var numberOfRequests = helpRequests.Count;

            for (var i = 0; i < numberOfRequests; i++)
            {
                if (string.IsNullOrWhiteSpace(helpRequests[i].AssignedTo))
                {
                    var patchRequestObject =
                        new PatchHelpRequest {AssignedTo = command.StaffMembers[i % numberOfStaff]};
                    _gateway.PatchHelpRequest(helpRequests[i].Id, patchRequestObject);
                }
            }
        }
    }
}
