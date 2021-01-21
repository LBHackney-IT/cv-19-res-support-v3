using System.Collections.Generic;
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
        private IUpdateCaseNoteUseCase _updateCaseNotetUseCase;

        public UpdateResidentAndHelpRequestUseCase(IUpdateResidentUseCase updateResidentUseCase,
            IUpdateHelpRequestUseCase updateHelpRequestUseCase, IUpdateCaseNoteUseCase updateCaseNoteUseCase)
        {
            _updateResidentUseCase = updateResidentUseCase;
            _updateHelpRequestUseCase = updateHelpRequestUseCase;
            _updateCaseNotetUseCase = updateCaseNoteUseCase;
        }

        public HelpRequestWithResident Execute(UpdateResidentAndHelpRequest command)
        {
            var helpRequest = _updateHelpRequestUseCase.Execute(command.Id, command.ToUpdateHelpRequestCommand());
            var resident = _updateResidentUseCase.Execute(helpRequest.ResidentId, command.ToUpdateResidentCommand());
            var caseNotes = _updateCaseNotetUseCase.Execute(helpRequest.Id, resident.Id, command.CaseNotes);
            if (caseNotes != null)
            {
                resident.CaseNotes = new List<ResidentCaseNote> { caseNotes };
            }
            return helpRequest.ToDomain(resident);
        }
    }
}
