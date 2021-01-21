using System;
using Amazon.Lambda.Core;
using cv19ResSupportV3.V3.Boundary.Response;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;

namespace cv19ResSupportV3.V3.UseCase
{
    public class GetResidentAndHelpRequestUseCase : IGetResidentAndHelpRequestUseCase
    {
        private readonly IHelpRequestGateway _gateway;

        public GetResidentAndHelpRequestUseCase(IHelpRequestGateway gateway)
        {
            _gateway = gateway;
        }

        public HelpRequestWithResident Execute(int id)
        {
            return _gateway.GetHelpRequest(id);
        }
    }
}
