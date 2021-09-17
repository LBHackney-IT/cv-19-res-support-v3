using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.Core;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Infrastructure;
using Microsoft.EntityFrameworkCore;

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

        public ResidentCaseNote CreateCaseNote(int residentId, int helpRequestId, string caseNote)
        {
            try
            {
                var rec = new CaseNoteEntity()
                {
                    ResidentId = residentId,
                    HelpRequestId = helpRequestId,
                    CaseNote = caseNote
                };

                _helpRequestsContext.CaseNoteEntities.Add(rec);
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


        public ResidentCaseNote UpdateCaseNote(int helpRequestId, int residentId, string caseNote)
        {
            try
            {
                var rec = _helpRequestsContext.CaseNoteEntities.FirstOrDefault(x => x.HelpRequestId == helpRequestId);
                if (rec == null)
                {
                    rec = new CaseNoteEntity();
                    _helpRequestsContext.CaseNoteEntities.Add(rec);
                }

                rec.ResidentId = residentId;
                rec.HelpRequestId = helpRequestId;
                rec.CaseNote = caseNote;
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

        public List<ResidentCaseNote> GetByResidentId(int residentId)
        {
            try
            {
                return _helpRequestsContext.CaseNoteEntities
                    .Where(x => x.ResidentId == residentId)
                    .Include(x => x.HelpRequestEntity).ToList().ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetByResidentId error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public List<ResidentCaseNote> GetByHelpRequestId(int helpRequestId)
        {
            try
            {
                return _helpRequestsContext.CaseNoteEntities.Where(x => x.HelpRequestId == helpRequestId).ToList().ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetByHelpRequestId error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }
    }
}
