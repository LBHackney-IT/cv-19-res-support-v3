using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class GetHelpRequestCallsUseCase : IGetHelpRequestCallsUseCase
    {
        private readonly IHelpRequestCallGateway _gateway;

        public GetHelpRequestCallsUseCase(IHelpRequestCallGateway gateway)
        {
            _gateway = gateway;
        }
        public List<CallGetResponse> Execute(int id)
        {
            return _gateway.GetHelpRequestCalls(id).ToResponse();
        }
    }
}
