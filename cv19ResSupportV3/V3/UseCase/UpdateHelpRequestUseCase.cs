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

        //TODO: rename id to the name of the identifier that will be used for this API, the type may also need to change
        public HelpRequest Execute(HelpRequest request)
        {
            return _gateway.UpdateHelpRequest(request.ToEntity());
        }
    }
}
