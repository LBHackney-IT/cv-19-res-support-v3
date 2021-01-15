using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Domain.Queries;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Gateways
{
    public interface IHelpRequestGateway
    {
        int CreateHelpRequest(int residentId, CreateHelpRequest command);
        List<LookupDomain> GetLookups(LookupQuery command);
        List<HelpRequest> GetCallbacks(CallbackQuery command);
        HelpRequest UpdateHelpRequest(UpdateHelpRequest command);
        HelpRequest GetHelpRequest(int id);
        Resident GetResident(int id);
        List<HelpRequest> SearchHelpRequests(SearchRequest command);
        void PatchHelpRequest(int id, PatchHelpRequest command);
        int? FindResident(FindResident command);
        Resident CreateResident(CreateResident command);
        Resident UpdateResident(int residentId , UpdateResident command);
    }
}
