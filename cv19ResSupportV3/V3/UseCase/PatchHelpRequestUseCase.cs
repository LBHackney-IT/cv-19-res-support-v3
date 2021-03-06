using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class PatchHelpRequestUseCase : IPatchHelpRequestUseCase
    {
        private IHelpRequestGateway _gateway;
        public PatchHelpRequestUseCase(IHelpRequestGateway gateway)
        {
            _gateway = gateway;
        }

        public HelpRequest Execute(int id, PatchHelpRequest command)
        {
            return _gateway.PatchHelpRequest(id, command);
        }
    }
}
