using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class CreateHelpRequestCallUseCase : ICreateHelpRequestCallUseCase
    {
        private IHelpRequestCallGateway _gateway;
        public CreateHelpRequestCallUseCase(IHelpRequestCallGateway gateway)
        {
            _gateway = gateway;
        }
        public HelpRequestCallCreateResponse Execute(HelpRequestCall request)
        {
            var response =  _gateway.CreateHelpRequestCall(request.ToEntity());
            return new HelpRequestCallCreateResponse
            {
                Id = response
            };
        }
    }
}
