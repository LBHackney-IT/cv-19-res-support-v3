using System;
using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;

namespace cv19ResSupportV3.V3.UseCase
{
    public class GetHelpRequestsUseCase : IGetHelpRequestsUseCase
    {
        private readonly IHelpRequestGateway _gateway;

        public GetHelpRequestsUseCase(IHelpRequestGateway gateway)
        {
            _gateway = gateway;
        }

        public List<HelpRequest> Execute(SearchRequest command)
        {
            if (command.Postcode == null && command.FirstName == null && command.LastName == null)
            {
                return new List<HelpRequest>();
            }
            return _gateway.SearchHelpRequests(command);
        }
    }
}
