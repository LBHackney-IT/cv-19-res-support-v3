using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.UseCase.Interface;

namespace cv19ResSupportV3.V4.UseCase
{
    public class GetResidentsUseCase : IGetResidentsUseCase
    {
        private readonly IResidentGateway _gateway;

        public GetResidentsUseCase(IResidentGateway gateway)
        {
            _gateway = gateway;
        }
        public ResidentResponseBoundary Execute(int id)
        {
            var gwResponse = _gateway.GetResident(id);
            return gwResponse.ToResponse();
        }
    }
}
