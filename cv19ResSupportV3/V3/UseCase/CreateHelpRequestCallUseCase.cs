using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class CreateHelpRequestCallUseCase : ICreateHelpRequestCallUseCase
    {
        private IHelpRequestCallGateway _helpRequestCallGateway;
        public CreateHelpRequestCallUseCase(IHelpRequestCallGateway helpRequestCallGateway)
        {
            _helpRequestCallGateway = helpRequestCallGateway;
        }
        public int Execute(int id, CreateHelpRequestCall command)
        {
            return _helpRequestCallGateway.CreateHelpRequestCall(id, command.ToEntity());
        }
    }
}
