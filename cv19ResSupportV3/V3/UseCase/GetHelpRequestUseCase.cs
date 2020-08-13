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

        public HelpRequestGetResponse Execute(int id)
        {
            return _gateway.GetHelpRequest(id).ToResponse();
        }
    }
}
