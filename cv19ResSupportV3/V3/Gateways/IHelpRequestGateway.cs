using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Gateways
{
    public interface IHelpRequestGateway
    {
        int CreateHelpRequest(CreateHelpRequest command);
        List<LookupEntity> GetLookups(LookupQueryParams requestParams);
        List<HelpRequest> GetCallbacks(CallbackRequestParams requestParams);
        HelpRequest UpdateHelpRequest(UpdateHelpRequest command);
        HelpRequest GetHelpRequest(int id);
        List<HelpRequest> SearchHelpRequests(RequestQueryParams queryParams);
        void PatchHelpRequest(int id, PatchHelpRequest request);
    }
}
