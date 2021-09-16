using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4.Boundary.Response;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.UseCase.Enumeration;
using cv19ResSupportV3.V4.UseCase.Interface;

namespace cv19ResSupportV3.V4.UseCase
{
    public class GetResidentHelpRequestUseCase : IGetResidentHelpRequestUseCase
    {
        private readonly IHelpRequestGateway _helpRequestGateway;

        public GetResidentHelpRequestUseCase(IHelpRequestGateway helpRequestGateway)
        {
            _helpRequestGateway = helpRequestGateway;
        }

        public ResidentHelpRequestResponse Execute(int id, int helpRequestId, IEnumerable<string> excludedHelpRequestTypes)
        {
            if (_helpRequestGateway.GetHelpRequest(helpRequestId) is HelpRequestWithResident helpRequest
                && IsAuthorised(helpRequest, id, excludedHelpRequestTypes))
            {
                return helpRequest.ToResidentHelpRequestResponse();
            }

            return new ResidentHelpRequestResponse();
        }

        private static bool IsAuthorised(HelpRequestWithResident helpRequest, int residentId, IEnumerable<string> excludedHelpRequestTypes)
            => helpRequest.ResidentId == residentId &&
            !excludedHelpRequestTypes.Contains(helpRequest.HelpNeeded);
    }
}
