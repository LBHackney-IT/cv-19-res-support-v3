using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;

namespace cv19ResSupportV3.V3.UseCase
{
    public class GetResidentsAndHelpRequestsUseCase : IGetResidentsAndHelpRequestsUseCase
    {
        private readonly IHelpRequestGateway _gateway;

        public GetResidentsAndHelpRequestsUseCase(IHelpRequestGateway gateway)
        {
            _gateway = gateway;
        }

        public List<HelpRequestResponse> Execute(SearchRequest command)
        {
            if (command.Postcode == null && command.FirstName == null && command.LastName == null)
            {
                return new List<HelpRequestResponse>();
            }

            var helpRequests = _gateway.SearchHelpRequests(command);
            if (helpRequests == null) return new List<HelpRequestResponse>();

            var result = helpRequests.Select(helpRequest =>
                {
                    var resident = _gateway.GetResident(helpRequest.ResidentId);
                    return helpRequest.ToResponse(resident);
                }
            ).ToList();

            return result;
        }
    }
}
