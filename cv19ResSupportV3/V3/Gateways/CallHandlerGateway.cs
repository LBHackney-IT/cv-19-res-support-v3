using System;
using System.Collections.Generic;
using System.Linq;
using Amazon.Lambda.Core;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V4.Factories;
using cv19ResSupportV3.V3.Infrastructure;
using cv19ResSupportV3.V3.Domain.Commands;

namespace cv19ResSupportV3.V3.Gateways
{
    public class CallHandlerGateway : ICallHandlerGateway
    {
        private readonly HelpRequestsContext _helpRequestsContext;

        public CallHandlerGateway(HelpRequestsContext helpRequestsContext)
        {
            _helpRequestsContext = helpRequestsContext;
        }

        public CallHandlerResponse GetCallHandler(int id)
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

        public List<CallHandlerResponse> GetCallHandlers()
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

        public CallHandlerResponse UpdateCallHandler(CallHandlerCommand request)
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

        public CallHandlerResponse CreateCallHandler(CallHandlerCommand request)
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

        public bool DeleteCallHandler(int id)
        {
            try
            {
                var callHandler = _helpRequestsContext.CallHandlerEntities.Find(id);

                if (callHandler == null) return false;

                foreach (var ch in _helpRequestsContext.HelpRequestEntities.Where(x => x.CallHandlerId == id))
                {
                    ch.CallHandlerId = null;
                }

                _helpRequestsContext.CallHandlerEntities.Remove(callHandler);

                return _helpRequestsContext.SaveChanges() > 0;
            }
            catch (Exception e)
            {
                LambdaLogger.Log($"{nameof(DeleteCallHandler)} error: ");
                LambdaLogger.Log(e.Message);
                throw;
            }
        }
    }
}
