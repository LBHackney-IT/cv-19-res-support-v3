using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class PatchCaseNoteUseCase : IPatchCaseNoteUseCase
    {
        private IHelpRequestGateway _gateway;

        public PatchCaseNoteUseCase(IHelpRequestGateway gateway)
        {
            _gateway = gateway;
        }

        public ResidentCaseNote Execute(int helpRequestId, int residentId, string command)
        {
            var caseNote = _gateway.PatchCaseNote(helpRequestId, residentId, command);
            return caseNote;
        }
    }
}
