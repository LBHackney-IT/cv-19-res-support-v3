using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Gateways
{
    public interface IHelpRequestGateway
    {
        int CreateHelpRequest(HelpRequest request);
        List<LookupEntity> GetLookups(LookupQueryParams requestParams);
        List<HelpRequestEntity> GetCallbacks(CallbackRequestParams requestParams);
        HelpRequest UpdateHelpRequest(HelpRequest request);
        HelpRequestEntity GetHelpRequest(int id);
        List<HelpRequestEntity> SearchHelpRequests(RequestQueryParams queryParams);
        void PatchHelpRequest(int id, HelpRequest request);
    }
}
