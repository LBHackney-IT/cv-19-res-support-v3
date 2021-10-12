using System.Collections.Generic;
using System.Linq;
using cv19ResSupportV3.V3.Domain;
using cv19ResSupportV3.V3.Domain.Commands;
using cv19ResSupportV3.V3.Infrastructure;

namespace cv19ResSupportV3.V4.Factories
{
    public static class CallHandlerFactory
    {
        public static CallHandlerResponseBoundary ToResponse(this CallHandler domain)
        {
            return new CallHandlerResponseBoundary
            {
                Id = domain.Id,
                Name = domain.Name,
                Email = domain.Email,
            };
        }

        public static CallHandler ToDomain(this CallHandlerEntity callHandler)
        {
            return new CallHandler()
            {
                Id = callHandler.Id,
                Name = callHandler.Name,
                Email = callHandler.Email,
            };
        }

        public static List<CallHandlerResponseBoundary> ToResponse(this IEnumerable<CallHandler> callhandlers)
        {
            return callhandlers.Select(x => x.ToResponse()).ToList();
        }
    }
}
