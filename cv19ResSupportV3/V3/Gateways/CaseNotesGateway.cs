using System;
using System.Linq;
using Amazon.Lambda.Core;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Gateways
{
    public class CaseNotesGateway : ICaseNotesGateway
    {
        private readonly HelpRequestsContext _helpRequestsContext;

        public CaseNotesGateway(HelpRequestsContext helpRequestsContext)
        {
            _helpRequestsContext = helpRequestsContext;
        }

        public ResidentCaseNote PatchCaseNote(int helpRequestId, int residentId, string caseNote)
        {
            try
            {
                var rec = _helpRequestsContext.CaseNoteEntities.FirstOrDefault(x => x.HelpRequestId == helpRequestId);
                if (rec == null)
                {
                    rec = new CaseNoteEntity();
                    rec.ResidentId = residentId;
                    rec.HelpRequestId = helpRequestId;
                    _helpRequestsContext.CaseNoteEntities.Add(rec);
                }
                if (caseNote != null)
                {
                    rec.CaseNote = caseNote;
                }
                _helpRequestsContext.SaveChanges();

                return rec.ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("PatchCaseNote error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }
    }
}
