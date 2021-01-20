using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class CreateCaseNoteUseCase : ICreateCaseNoteUseCase
    {
        private readonly ICaseNotesGateway _gateway;
        public CreateCaseNoteUseCase(ICaseNotesGateway gateway)
        {
            _gateway = gateway;
        }
        public ResidentCaseNote Execute(int helpRequestId, int residentId, string caseNoteContent)
        {
            var response = _gateway.CreateCaseNote(helpRequestId, residentId, caseNoteContent);
            return response;
        }
    }
}
