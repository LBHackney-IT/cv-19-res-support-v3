using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class UpdateResidentAndHelpRequestUseCase : IUpdateResidentAndHelpRequestUseCase
    {
        private IUpdateResidentUseCase _updateResidentUseCase;
        private IUpdateHelpRequestUseCase _updateHelpRequestUseCase;
        public UpdateResidentAndHelpRequestUseCase(IUpdateResidentUseCase updateResidentUseCase, IUpdateHelpRequestUseCase updateHelpRequestUseCase)
        {
            _updateResidentUseCase = updateResidentUseCase;
            _updateHelpRequestUseCase = updateHelpRequestUseCase;
        }

        public HelpRequestWithResident Execute(UpdateResidentAndHelpRequest command)
        {
            var resident =_updateResidentUseCase.Execute(command.ToUpdateResidentCommand());
            var helpRequest =_updateHelpRequestUseCase.Execute(command.ToUpdateHelpRequestCommand());
            return helpRequest.ToDomain(resident);
        }
    }
}
