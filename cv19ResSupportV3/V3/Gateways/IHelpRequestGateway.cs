using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Domain.Queries;

namespace cv19ResSupportV3.V3.Gateways
{
    public interface IHelpRequestGateway
    {
        int CreateHelpRequest(int residentId, CreateHelpRequest command);
        List<LookupDomain> GetLookups(LookupQuery command);
        HelpRequest UpdateHelpRequest(UpdateHelpRequest command);
        Resident GetResident(int id);
        void PatchHelpRequest(int id, PatchHelpRequest command);
        int? FindResident(FindResident command);
        Resident CreateResident(CreateResident command);
        Resident UpdateResident(int residentId, UpdateResident command);
        HelpRequestWithResident GetHelpRequest(int id);
        List<HelpRequestWithResident> SearchHelpRequests(SearchRequest command);
        List<HelpRequestWithResident> GetCallbacks(CallbackQuery command);

    }
}
