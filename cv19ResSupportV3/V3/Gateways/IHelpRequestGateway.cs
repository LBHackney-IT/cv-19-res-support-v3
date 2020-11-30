using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Gateways
{
    public interface IHelpRequestGateway
    {
        int CreateHelpRequest(HelpRequestEntity request);
        List<LookupEntity> GetLookups(LookupQueryParams requestParams);
        List<HelpRequestEntity> GetCallbacks(CallbackRequestParams requestParams);
        HelpRequestEntity UpdateHelpRequest(HelpRequestEntity request);
        HelpRequestEntity GetHelpRequest(int id);
        List<HelpRequestEntity> SearchHelpRequests(RequestQueryParams queryParams);
        void PatchHelpRequest(int id, HelpRequestEntity request);
    }
}
