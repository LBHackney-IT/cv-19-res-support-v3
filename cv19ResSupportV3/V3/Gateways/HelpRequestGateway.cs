using System.Collections.Generic;
using cv19ResRupportV3.V3.Domain;
using cv19ResRupportV3.V3.Factories;
using cv19ResRupportV3.V3.Infrastructure;
using HelpRequest = cv19ResRupportV3.V3.Domain.HelpRequest;

namespace cv19ResRupportV3.V3.Gateways
{
    public class HelpRequestGateway : IHelpRequestGateway
    {

        private readonly HelpRequestsContext _helpRequestsContext;

        public HelpRequestGateway(HelpRequestsContext helpRequestsContext)
        {
            _helpRequestsContext = helpRequestsContext;
        }

        public int CreateHelpRequest(HelpRequestEntity request)
        {
            _helpRequestsContext.HelpRequestEntities.Add(request);
            return request.Id;
        }
    }
}
