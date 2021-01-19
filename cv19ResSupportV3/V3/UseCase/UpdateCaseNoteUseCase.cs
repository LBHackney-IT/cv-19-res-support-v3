using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.UseCase.Interfaces;

namespace cv19ResSupportV3.V3.UseCase
{
    public class UpdateCaseNoteUseCase : IUpdateCaseNoteUseCase
    {

        private readonly ICaseNotesGateway _gateway;
        public UpdateCaseNoteUseCase(ICaseNotesGateway gateway)
        {
            _gateway = gateway;
        }
        public ResidentCaseNote Execute(int id, int residentId, string command)
        {
            return _gateway.UpdateCaseNote(id, residentId, command);
        }
    }
}
