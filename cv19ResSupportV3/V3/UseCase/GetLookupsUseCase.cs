using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Queries;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;

namespace cv19ResSupportV3.V3.UseCase
{
    public class GetLookupsUseCase : IGetLookupsUseCase
    {
        private readonly IHelpRequestGateway _gateway;

        public GetLookupsUseCase(IHelpRequestGateway gateway)
        {
            _gateway = gateway;
        }

        public List<LookupDomain> Execute(LookupQuery query)
        {
            return _gateway.GetLookups(query);
        }
    }
}
