using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
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

        public void Execute(int id, HelpRequest request)
        {
            _gateway.PatchHelpRequest(id, request);
        }
    }
}
