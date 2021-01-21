using System;
using Amazon.Lambda.Core;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Factories.Commands;
using cv19ResSupportV3.V3.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace cv19ResSupportV3.V3.Gateways
{
    public class HelpRequestCallGateway : IHelpRequestCallGateway
    {

        private readonly HelpRequestsContext _helpRequestsContext;

        public HelpRequestCallGateway(HelpRequestsContext helpRequestsContext)
        {
            _helpRequestsContext = helpRequestsContext;
        }

        public int CreateHelpRequestCall(int id, CreateHelpRequestCall command)
        {
            if (command == null) return 0;
            try
            {
                var helpRequest = _helpRequestsContext.HelpRequestEntities.Find(id);
                if (helpRequest == null)
                    throw new InvalidOperationException();
                var entity = command.ToEntity();
                helpRequest.HelpRequestCalls.Add(entity);
                _helpRequestsContext.Entry(entity).State = EntityState.Added;
                _helpRequestsContext.SaveChanges();
                return entity.Id;
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
