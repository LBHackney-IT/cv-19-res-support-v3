using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4.UseCase.Enumeration;
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
        public List<ResidentCaseNote> Execute(int id, IEnumerable<string> excludedHelpTypes)
        {
            return _gateway.GetByHelpRequestId(id)
                ?.Where(x => !excludedHelpTypes.Contains(x.HelpNeeded)).ToList();
        }
    }
}
