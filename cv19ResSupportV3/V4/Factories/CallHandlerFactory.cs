using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V4.Factories
{
    public static class CallHandlerFactory
    {
        public static CallHandlerResponseBoundary ToResponse(this CallHandlerResponse domain)
        {
            return new CallHandlerResponseBoundary
            {
                Id = domain.Id,
                Name = domain.Name,
                Email = domain.Email,
            };
        }

        public static CallHandlerResponse ToDomain(this CallHandlerEntity callHandler)
        {
            return new CallHandlerResponse()
            {
                Id = callHandler.Id,
                Name = callHandler.Name,
                Email = callHandler.Email,
            };
        }

        public static List<CallHandlerResponseBoundary> ToResponse(this IEnumerable<CallHandlerResponse> callhandlers)
        {
            return callhandlers.Select(x => x.ToResponse()).ToList();
        }

        public static CallHandlerEntity ToEntity(this CallHandlerCommand request)
            => request.Id.HasValue ? new CallHandlerEntity()
            {
                Id = request.Id.Value,
                Name = request.Name,
                Email = request.Email,
            }
             : new CallHandlerEntity()
             {
                 Name = request.Name,
                 Email = request.Email,
             };

        public static CallHandlerCommand ToDomain(this CreateCallHandlerRequestBoundary request)
            => new CallHandlerCommand()
            {
                Name = request.Name,
                Email = request.Email,
            };

        public static CallHandlerCommand ToDomain(this PutCallHandlerRequestBoundary request)
            => new CallHandlerCommand()
            {
                Id = request.Id,
                Name = request.Name,
                Email = request.Email,
            };
    }
}
