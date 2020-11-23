using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Amazon.Lambda.Core;
using cv19ResSupportV3.V3.Boundary.Requests;
using cv19ResSupportV3.V3.Factories;
using cv19ResSupportV3.V3.Gateways;
using cv19ResSupportV3.V3.Infrastructure;
using HelpRequest = cv19ResSupportV3.V3.Domain.HelpRequest;

namespace cv19ResSupportV3.V3.Gateways
{
    public class HelpRequestCallGateway : IHelpRequestCallGateway
    {

        private readonly HelpRequestsContext _helpRequestsContext;

        public HelpRequestCallGateway(HelpRequestsContext helpRequestsContext)
        {
            _helpRequestsContext = helpRequestsContext;
        }

        public int CreateHelpRequestCall(HelpRequestCallEntity request)
        {
            if (request == null) return 0;
            try
            {
                _helpRequestsContext.HelpRequestCallEntities.Add(request);
                _helpRequestsContext.SaveChanges();
                return request.Id;
            }
            catch (Exception e)
            {
                LambdaLogger.Log("CreateHelpRequestCall error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }


    }
}
