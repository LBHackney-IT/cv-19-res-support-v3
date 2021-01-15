using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class CreateResidentUseCase : ICreateResidentUseCase
    {
        private IHelpRequestGateway _gateway;
        public CreateResidentUseCase(IHelpRequestGateway gateway)
        {
            _gateway = gateway;
        }
        public Resident Execute(CreateResident command)
        {
            var existingResidentId = _gateway.FindResident(command.ToFindResidentCommand());
            //get resident with id
            //update the resident fields
            //if numbers are same dodnt conc
            //if null dont add
            if (existingResidentId != null) return _gateway.UpdateResident((int) existingResidentId, command.ToUpdateResidentCommand());
            var resident = _gateway.CreateResident(command);
            return resident;
        }
    }
}
