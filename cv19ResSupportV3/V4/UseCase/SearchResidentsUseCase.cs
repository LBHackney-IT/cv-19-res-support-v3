using System.Collections.Generic;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4.Boundary.Requests;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.UseCase.Interface;

namespace cv19ResSupportV3.V4.UseCase
{
    public class SearchResidentsUseCase : ISearchResidentsUseCase
    {
        private readonly IResidentGateway _residentGateway;
        public SearchResidentsUseCase(IResidentGateway residentGateway)
        {
            _residentGateway = residentGateway;
        }

        public List<ResidentResponseBoundary> Execute(FindResident requestParams)
        {
            var gwResponse = _residentGateway.SearchResidents(requestParams);
            return gwResponse == null ? new List<ResidentResponseBoundary>() : gwResponse.ToResponse();
        }
    }
}
