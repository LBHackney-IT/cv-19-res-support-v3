using System;
using Amazon.Lambda.Core;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V4.Boundary.Requests;
using cv19ResSupportV3.V4.Boundary.Response;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V4.UseCase.Interface;

namespace cv19ResSupportV3.V4.UseCase
{
    public class PatchResidentHelpRequestUseCase : IPatchResidentHelpRequestUseCase
    {
        private readonly IHelpRequestGateway _helpRequestGateway;
        public PatchResidentHelpRequestUseCase(IHelpRequestGateway helpRequestGateway)
        {
            _helpRequestGateway = helpRequestGateway;
        }
        public ResidentHelpRequestResponse Execute(int id, int helpRequestId, ResidentHelpRequestRequest request)
        {
            try
            {
                var gwResponse = _helpRequestGateway.PatchHelpRequest(helpRequestId, request.ToPatchHelpRequest());
                return gwResponse.ToResponse();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("PatchResidentHelpRequestUseCase execute error: ");
                LambdaLogger.Log(e.Message);
                return null;
            }
        }
    }
}
