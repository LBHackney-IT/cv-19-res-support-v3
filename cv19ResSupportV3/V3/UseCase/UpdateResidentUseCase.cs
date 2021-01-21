using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class UpdateResidentUseCase : IUpdateResidentUseCase
    {
        private IResidentGateway _gateway;
        public UpdateResidentUseCase(IResidentGateway gateway)
        {
            _gateway = gateway;
        }

        public Resident Execute(int id, UpdateResident command)
        {
            return _gateway.UpdateResident(id, command);
        }
    }
}
