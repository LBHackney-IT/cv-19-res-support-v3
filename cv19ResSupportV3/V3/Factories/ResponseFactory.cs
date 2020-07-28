using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Factories
{
    public static class ResponseFactory
    {
        public static HelpRequest ToResponse(this HelpRequestEntity hr)
        {
            return hr.ToDomain();
        }

        public static List<HelpRequest> ToResponse(this IEnumerable<HelpRequestEntity> domainList)
        {
            return domainList.Select(domain => domain.ToResponse()).ToList();
        }
    }
}
