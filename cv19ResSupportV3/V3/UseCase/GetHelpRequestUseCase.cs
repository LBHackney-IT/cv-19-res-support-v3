using System;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;

namespace cv19ResSupportV3.V3.UseCase
{
    public class GetHelpRequestUseCase : IGetHelpRequestUseCase
    {
        private readonly IHelpRequestGateway _gateway;

        public GetHelpRequestUseCase(IHelpRequestGateway gateway)
        {
            _gateway = gateway;
        }

        public HelpRequestResponse Execute(int id)
        {
            var helpRequest = _gateway.GetHelpRequest(id);
            var resident = _gateway.GetResident(helpRequest.ResidentId);

            var result = helpRequest.ToResponse(resident);

            return result;
        }
    }
}
