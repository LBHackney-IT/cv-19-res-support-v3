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
        public ResidentCaseNote Execute(int residentId, int helpRequestId, string caseNoteContent)
        {
            var response = _gateway.CreateCaseNote(residentId, helpRequestId, caseNoteContent);
            return response;
        }
    }
}
