using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.UseCase.Interface;

namespace cv19ResSupportV3.V4.UseCase
{
    public class CreateResidentsUseCase : ICreateResidentsUseCase
    {
        private readonly IResidentGateway _gateway;

        public CreateResidentsUseCase(IResidentGateway gateway)
        {
            _gateway = gateway;
        }
        public ResidentResponseBoundary Execute(ResidentRequestBoundary request)
        {
            var gwResponse = _gateway.CreateResident(request.ToCommand());
            return gwResponse.ToResponse();
        }
    }
}
