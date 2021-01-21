using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
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

        public List<HelpRequestWithResident> Execute(SearchRequest command)
        {
            if (command.Postcode == null && command.FirstName == null && command.LastName == null)
            {
                return new List<HelpRequestWithResident>();
            }

            return _gateway.SearchHelpRequests(command);
        }
    }
}
