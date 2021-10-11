using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Amazon.Lambda.Core;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V4.Helpers;
using Microsoft.EntityFrameworkCore;

namespace cv19ResSupportV3.V3.Gateways
{
    public class CallHandlerGateway : ICallHandlerGateway
    {
        private readonly HelpRequestsContext _helpRequestsContext;

        public CallHandlerGateway(HelpRequestsContext helpRequestsContext)
        {
            _helpRequestsContext = helpRequestsContext;
        }

        public List<CallHandler> GetCallHandlers()
        {
            try
            {
                var response = _helpRequestsContext.CallHandlerEntities
                    .Select(ch => ch.ToDomain()).ToList();
                return response;
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetCallHandlers error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }
    }
}
