using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
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

        public HelpRequest Execute(UpdateHelpRequest command)
        {
            return _gateway.UpdateHelpRequest(command);
        }
    }
}
