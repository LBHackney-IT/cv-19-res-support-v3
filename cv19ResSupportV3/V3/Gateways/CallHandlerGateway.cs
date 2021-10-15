using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.Core;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V3.Gateways
{
    public class CallHandlerGateway : ICallHandlerGateway
    {
        private readonly HelpRequestsContext _helpRequestsContext;

        public CallHandlerGateway(HelpRequestsContext helpRequestsContext)
        {
            _helpRequestsContext = helpRequestsContext;
        }

        public CallHandler GetCallHandler(int id)
        {
            try
            {
                return _helpRequestsContext.CallHandlerEntities
                        .FirstOrDefault(ch => ch.Id == id)
                        ?.ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log("GetCallHandlers error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
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

        public CallHandler UpdateCallHandler(CallHandler request)
        {
            if (request == null) return null;

            try
            {
                var entity = _helpRequestsContext.CallHandlerEntities.Find(request.Id);
                _helpRequestsContext.Entry(entity).CurrentValues.SetValues(request);
                _helpRequestsContext.SaveChanges();
                return entity.ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log($"{nameof(UpdateCallHandler)} error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }

        public CallHandler CreateCallHandler(CallHandler request)
        {
            var requestEntity = request?.ToEntity();
            if (requestEntity == null) return null;

            try
            {
                _helpRequestsContext.CallHandlerEntities.Add(requestEntity);
                _helpRequestsContext.SaveChanges();

                return requestEntity.ToDomain();
            }
            catch (Exception e)
            {
                LambdaLogger.Log($"{nameof(CreateCallHandler)} error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }
    }
}
