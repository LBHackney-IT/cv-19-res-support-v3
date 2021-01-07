using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class UpdateHelpRequestUseCase : IUpdateHelpRequestUseCase
    {
        private IHelpRequestGateway _gateway;
        public UpdateHelpRequestUseCase(IHelpRequestGateway gateway)
        {
            _gateway = gateway;
        }

        public HelpRequest Execute(HelpRequest request)
        {
            return _gateway.UpdateHelpRequest(request);
        }
    }
}
