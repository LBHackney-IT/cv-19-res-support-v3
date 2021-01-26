using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4.Boundary.Response;
using cv19ResSupportV3.V4.Factories;
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
        public ResidentHelpRequestResponse Execute(int id, int helpRequestId)
        {
            var helpRequest = _helpRequestGateway.GetHelpRequest(helpRequestId);
            if (helpRequest == null)
                return null;
            if (helpRequest.ResidentId != id)
                return null;
            return helpRequest.ToResidentHelpRequestResponse();
        }
    }
}
