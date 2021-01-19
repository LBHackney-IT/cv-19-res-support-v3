using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class PatchResidentUseCase : IPatchResidentUseCase
    {
        private IResidentGateway _gateway;
        public PatchResidentUseCase(IResidentGateway gateway)
        {
            _gateway = gateway;
        }

        public Resident Execute(int id, PatchResident command)
        {
            return _gateway.PatchResident(id, command);
        }
    }
}
