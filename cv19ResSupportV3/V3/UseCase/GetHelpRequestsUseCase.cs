using System;
using System.Collections.Generic;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Boundary.Response;
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

        public List<HelpRequestGetResponse> Execute(RequestQueryParams queryParams)
        {
            if(queryParams == null)
                return new List<HelpRequestGetResponse>();
            return queryParams.PostCode == null ? new List<HelpRequestGetResponse>()
                : _gateway.SearchHelpRequests(queryParams.PostCode).ToResponse();
        }
    }
}
