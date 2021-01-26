using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Domain.Queries;

namespace cv19ResSupportV3.V3.Gateways
{
    public interface IHelpRequestGateway
    {
        int CreateHelpRequest(int residentId, CreateHelpRequest command);
        int? FindHelpRequestByCtasId(string ctasId);
        List<LookupDomain> GetLookups(LookupQuery command);
        HelpRequest UpdateHelpRequest(int id, UpdateHelpRequest command);
        HelpRequest PatchHelpRequest(int id, PatchHelpRequest command);
        HelpRequestWithResident GetHelpRequest(int id);
        List<HelpRequestWithResident> SearchHelpRequests(SearchRequest command);
        List<HelpRequestWithResident> GetCallbacks(CallbackQuery command);
        List<HelpRequestWithResident> GetResidentHelpRequests(int id);
    }
}
