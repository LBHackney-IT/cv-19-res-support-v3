using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class CreateResidentAndHelpRequestUseCase : ICreateResidentAndHelpRequestUseCase
    {
        private readonly ICreateResidentUseCase _createResidentUseCase;
        private readonly ICreateHelpRequestUseCase _createHelpRequestUseCase;
        private readonly ICreateCaseNoteUseCase _createCaseNote;


        public CreateResidentAndHelpRequestUseCase(ICreateResidentUseCase createResidentUseCase, ICreateHelpRequestUseCase createHelpRequestUseCase, ICreateCaseNoteUseCase createCaseNote)
        {
            _createResidentUseCase = createResidentUseCase;
            _createHelpRequestUseCase = createHelpRequestUseCase;
            _createCaseNote = createCaseNote;
        }
        public int Execute(CreateResidentAndHelpRequest command)
        {
            var resident = _createResidentUseCase.Execute(command.ToCreateResidentCommand());

            var helpRequestId = _createHelpRequestUseCase.Execute(resident.Id, command.ToCreateHelpRequestCommand());
            if (command.CaseNotes != null)
            {
                _createCaseNote.Execute(resident.Id, helpRequestId, command.CaseNotes);
            }

            return helpRequestId;
        }
    }
}
