using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;

namespace cv19ResSupportV3.V3.UseCase
{
    public class GetHelpRequestsUseCase : IGetHelpRequestsUseCase
    {
        private readonly IHelpRequestGateway _gateway;

        public GetHelpRequestsUseCase(IHelpRequestGateway gateway)
        {
            _gateway = gateway;
        }

        public HelpRequestResponseList Execute()
        {
            return new HelpRequestResponseList() { HelpRequestObjects = _gateway.GetHelpRequests().ToResponse()};
        }
    }
}
