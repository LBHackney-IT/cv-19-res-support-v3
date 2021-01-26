using System.Collections.Generic;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4.Boundary.Response;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.UseCase.Interface;

namespace cv19ResSupportV3.V4.UseCase
{
    public class GetResidentHelpRequestsUseCase : IGetResidentHelpRequestsUseCase
    {
        private readonly IHelpRequestGateway _helpRequestGateway;
        public GetResidentHelpRequestsUseCase(IHelpRequestGateway helpRequestGateway)
        {
            _helpRequestGateway = helpRequestGateway;
        }
        public List<ResidentHelpRequestResponse> Execute(int id)
        {
            var residentHelpRequests = _helpRequestGateway.GetResidentHelpRequests(id);
            return residentHelpRequests == null
                ? new List<ResidentHelpRequestResponse>()
                : residentHelpRequests.ToResidentHelpRequestResponse();
        }
    }
}
