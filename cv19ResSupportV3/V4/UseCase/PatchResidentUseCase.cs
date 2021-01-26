using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.UseCase.Interfaces;

namespace cv19ResSupportV3.V4.UseCase
{
    public class PatchResidentUseCase : IPatchResidentUseCase
    {
        private readonly IResidentGateway _residentGateway;
        public PatchResidentUseCase(IResidentGateway residentGateway)
        {
            _residentGateway = residentGateway;
        }

        public ResidentResponseBoundary Execute(int id, ResidentRequestBoundary command)
        {
            var gwResponse = _residentGateway.PatchResident(id, command.ToPatchResident());
            return gwResponse.ToResponse();
        }
    }
}
