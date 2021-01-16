using System.Collections.Generic;
using System.Linq;
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

        public List<HelpRequestWithResident> Execute(CallbackQuery command)
        {
            return _gateway.GetCallbacks(command);
        }
    }
}
