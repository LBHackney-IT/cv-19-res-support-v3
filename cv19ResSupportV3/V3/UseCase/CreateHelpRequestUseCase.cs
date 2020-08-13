using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class CreateHelpRequestUseCase : ICreateHelpRequestUseCase
    {
        private IHelpRequestGateway _gateway;
        public CreateHelpRequestUseCase(IHelpRequestGateway gateway)
        {
            _gateway = gateway;
        }
        public HelpRequestCreateResponse Execute(HelpRequest request)
        {
            var response =  _gateway.CreateHelpRequest(request.ToEntity());
            return new HelpRequestCreateResponse
            {
                Id = response
            };
        }
    }
}
