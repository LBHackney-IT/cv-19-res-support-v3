using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.Factories.Commands
{
    public static class SearchRequestFactory
    {
        public static SearchRequest ToCommand(this RequestQueryParams request)
        {
            return new SearchRequest()
            {
                Postcode = request.Postcode,
                FirstName = request.FirstName,
                LastName = request.LastName,
                HelpNeeded = request.HelpNeeded
            };
        }
    }
}
