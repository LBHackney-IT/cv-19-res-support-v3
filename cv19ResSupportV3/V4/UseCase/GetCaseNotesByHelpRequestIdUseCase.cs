using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4.UseCase.Interface;

namespace cv19ResSupportV3.V4.UseCase
{
    public class GetCaseNotesByHelpRequestIdUseCase : IGetCaseNotesByHelpRequestIdUseCase
    {
        private readonly ICaseNotesGateway _gateway;

        public GetCaseNotesByHelpRequestIdUseCase(ICaseNotesGateway gateway)
        {
            _gateway = gateway;
        }
        public List<ResidentCaseNote> Execute(int id)
        {
            return _gateway.GetByHelpRequestId(id);
        }
    }
}
