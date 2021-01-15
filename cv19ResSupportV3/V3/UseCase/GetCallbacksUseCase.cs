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
    public class GetCallbacksUseCase : IGetCallbacksUseCase
    {
        private readonly IHelpRequestGateway _gateway;

        public GetCallbacksUseCase(IHelpRequestGateway gateway)
        {
            _gateway = gateway;
        }

        public List<HelpRequestResponse> Execute(CallbackQuery command)
        {
            var helpRequests = _gateway.GetCallbacks(command);
            var result = helpRequests?.Select(hr => hr.ToResponse(_gateway.GetResident(hr.ResidentId))).ToList();
            return result;
        }
    }
}
