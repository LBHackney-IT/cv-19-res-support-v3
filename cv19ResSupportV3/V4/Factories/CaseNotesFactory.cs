using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V4.Boundary.Responses;

namespace cv19ResSupportV3.V4.Factories
{
    public static class CaseNotesFactory
    {
        public static CaseNoteResponse ToResponse(this ResidentCaseNote note)
        {
            return note == null
                ? null
                : new CaseNoteResponse()
                {
                    Id = note.Id,
                    HelpRequestId = note.HelpRequestId,
                    ResidentId = note.ResidentId,
                    CaseNote = note.CaseNote
                };
        }

        public static List<CaseNoteResponse> ToResponse(this ICollection<ResidentCaseNote> notes)
        {
            return notes?.Select(n => n.ToResponse()).ToList();
        }

    }
}
