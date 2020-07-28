using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Gateways
{
    public interface IHelpRequestGateway
    {
        int CreateHelpRequest(HelpRequestEntity request);
        List<HelpRequestEntity> GetHelpRequests();
        List<HelpRequestEntity> GetCallbacks();
        HelpRequestEntity UpdateHelpRequest(HelpRequestEntity request);
        HelpRequestEntity GetHelpRequest(int id);
    }
}
