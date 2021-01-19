using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Domain.Queries;

namespace cv19ResSupportV3.V3.Gateways
{
    public interface ICaseNotesGateway
    {
        ResidentCaseNote PatchCaseNote(int helpRequestId, int residentId, string caseNote);
        ResidentCaseNote CreateCaseNote(int helpRequestId, int residentId, string caseNote);
    }
}
